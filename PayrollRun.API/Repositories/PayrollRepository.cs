using System.Data;
using Dapper;
using PayrollRun.API.Data;
using PayrollRun.API.DTOs;
using PayrollRun.API.Repositories.Interfaces;

namespace PayrollRun.API.Repositories;

public class PayrollRepository : IPayrollRepository
{
    private readonly DapperContext _context;

    public PayrollRepository(DapperContext context)
    {
        _context = context;
    }

    // ----------------------------------------
    // Check payroll exists
    // ----------------------------------------
    public async Task<bool> IsPayrollExistsAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT COUNT(1)
            FROM PayrollRuns
            WHERE PayrollMonth = @Month
              AND PayrollYear = @Year";

        return await connection.ExecuteScalarAsync<int>(sql, new { Month = month, Year = year }) > 0;
    }

    // ----------------------------------------
    // Run Payroll (Stored Procedure)
    // ----------------------------------------
    public async Task RunPayrollAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            "dbo.usp_RunPayroll",
            new { Month = month, Year = year },
            commandType: CommandType.StoredProcedure);
    }

    // ----------------------------------------
    // Get Payroll Summary (Month/Year based)
    // ----------------------------------------
    public async Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT
                pd.EmployeeId,
                e.EmployeeName,
                pd.BasicSalary,
                pd.WorkingDays,
                pd.DaysPresent,
                pd.GrossPay,
                pd.PFDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollDetails pd
            INNER JOIN PayrollRuns pr ON pr.PayrollRunId = pd.PayrollRunId
            INNER JOIN Employees e ON e.EmployeeId = pd.EmployeeId
            WHERE pr.PayrollMonth = @Month
              AND pr.PayrollYear = @Year
            ORDER BY e.EmployeeName";

        return await connection.QueryAsync<PayrollResponseDto>(
            sql,
            new { Month = month, Year = year });
    }

    // ----------------------------------------
    // FIXED: Payslip (IMPORTANT FIX)
    // DO NOT depend only on RunId externally
    // ----------------------------------------
    public async Task<PayslipDto?> GetPayslipAsync(int month, int year, int employeeId)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT TOP 1
                pd.EmployeeId,
                e.EmployeeName,
                pd.BasicSalary,
                pd.WorkingDays,
                pd.DaysPresent,
                pd.GrossPay,
                pd.PFDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollDetails pd
            INNER JOIN PayrollRuns pr ON pr.PayrollRunId = pd.PayrollRunId
            INNER JOIN Employees e ON e.EmployeeId = pd.EmployeeId
            WHERE pr.PayrollMonth = @Month
              AND pr.PayrollYear = @Year
              AND pd.EmployeeId = @EmployeeId
            ORDER BY pr.PayrollRunId DESC";

        return await connection.QueryFirstOrDefaultAsync<PayslipDto>(
            sql,
            new { Month = month, Year = year, EmployeeId = employeeId });
    }

    // ----------------------------------------
    // Finalize Payroll (SAFE)
    // ----------------------------------------
    public async Task FinalizePayrollAsync(int payrollRunId)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            UPDATE PayrollRuns
            SET IsFinalized = 1
            WHERE PayrollRunId = @PayrollRunId
              AND ISNULL(IsFinalized, 0) = 0";

        await connection.ExecuteAsync(sql, new { PayrollRunId = payrollRunId });
    }

    // ----------------------------------------
    // Check Finalized
    // ----------------------------------------
    public async Task<bool> IsPayrollFinalizedAsync(int payrollRunId)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT ISNULL(IsFinalized, 0)
            FROM PayrollRuns
            WHERE PayrollRunId = @PayrollRunId";

        return await connection.ExecuteScalarAsync<bool>(
            sql,
            new { PayrollRunId = payrollRunId });
    }

    
}