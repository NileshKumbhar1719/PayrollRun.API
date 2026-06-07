using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
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

    public async Task<bool> IsPayrollExistsAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT COUNT(1)
            FROM PayrollRuns
            WHERE PayrollMonth = @Month
              AND PayrollYear = @Year";

        var count = await connection.ExecuteScalarAsync<int>(
            sql,
            new { Month = month, Year = year });

        return count > 0;
    }

    public async Task RunPayrollAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        var exists = await IsPayrollExistsAsync(month, year);

        if (exists)
            throw new InvalidOperationException(
                $"Payroll already processed for {month}/{year}");

        await connection.ExecuteAsync(
            "usp_RunPayroll",
            new { Month = month, Year = year },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(int month, int year)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT
                pd.EmployeeId,
                e.EmployeeName AS employeeName,
                pd.BasicSalary,
                pd.WorkingDays,
                pd.DaysPresent,
                pd.GrossPay,
                pd.PFDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollDetails pd
            INNER JOIN PayrollRuns pr
                ON pd.PayrollRunId = pr.PayrollRunId
            INNER JOIN Employees e
                ON pd.EmployeeId = e.EmployeeId
            WHERE pr.PayrollMonth = @Month
              AND pr.PayrollYear = @Year";

        return await connection.QueryAsync<PayrollResponseDto>(
            sql,
            new { Month = month, Year = year });
    }

    public async Task<PayslipDto?> GetPayslipAsync(int runId, int employeeId)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
            SELECT
                pd.EmployeeId,
                e.EmployeeName AS employeeName,
                pd.BasicSalary,
                pd.WorkingDays,
                pd.DaysPresent,
                pd.GrossPay,
                pd.PFDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollDetails pd
            INNER JOIN Employees e
                ON e.EmployeeId = pd.EmployeeId
            WHERE pd.PayrollRunId = @RunId
              AND pd.EmployeeId = @EmployeeId";

        return await connection.QueryFirstOrDefaultAsync<PayslipDto>(
            sql,
            new { RunId = runId, EmployeeId = employeeId });
    }
}