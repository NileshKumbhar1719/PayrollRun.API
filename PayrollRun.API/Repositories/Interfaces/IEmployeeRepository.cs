using PayrollRun.API.Models;

namespace PayrollRun.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
