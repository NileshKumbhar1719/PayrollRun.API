using PayrollRun.API.Models;

namespace PayrollRun.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
    }
}
