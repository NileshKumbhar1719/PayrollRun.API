using PayrollRun.API.Models;
using PayrollRun.API.Repositories.Interfaces;
using PayrollRun.API.Services.Interfaces;

namespace PayrollRun.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _Repository;

        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _Repository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _Repository.GetEmployeesAsync();
        }
    }
}
