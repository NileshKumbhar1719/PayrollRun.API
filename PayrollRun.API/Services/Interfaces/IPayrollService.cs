using PayrollRun.API.DTOs;

namespace PayrollRun.API.Services.Interfaces
{
    public interface IPayrollService
    {
        Task RunPayrollAsync(int month, int year);

        Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(int month, int year);

        Task<PayslipDto?> GetPayslipAsync(int runId, int employeeId);
    }
}
