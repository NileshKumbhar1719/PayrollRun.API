using Dapper;
using PayrollRun.API.Data;
using PayrollRun.API.Models;
using PayrollRun.API.Repositories.Interfaces;

namespace PayrollRun.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;

    public EmployeeRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        using var connection = _context.CreateConnection();

        var sql = @"
            SELECT
                EmployeeId,
                EmployeeCode,
                EmployeeName,
                DepartmentId,
                BasicSalary,
                IsActive
            FROM Employees
            WHERE IsActive = 1";

        return await connection.QueryAsync<Employee>(sql);
    }
}