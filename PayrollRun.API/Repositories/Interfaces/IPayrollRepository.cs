using PayrollRun.API.DTOs;

namespace PayrollRun.API.Repositories.Interfaces
{
    public interface IPayrollRepository
    {
        Task RunPayrollAsync(int month, int year);

        Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(int month,int year);

        Task<PayslipDto?> GetPayslipAsync(int runId, int employeeId);
    }
}
