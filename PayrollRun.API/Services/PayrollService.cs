using PayrollRun.API.DTOs;
using PayrollRun.API.Repositories.Interfaces;
using PayrollRun.API.Services.Interfaces;

namespace PayrollRun.API.Services;

public class PayrollService : IPayrollService
{
    private readonly IPayrollRepository _payrollRepository;

    public PayrollService(IPayrollRepository payrollRepository)
    {
        _payrollRepository = payrollRepository;
    }

    public async Task RunPayrollAsync(int month, int year)
    {
        await _payrollRepository.RunPayrollAsync(month, year);
    }

    public async Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(int month, int year)
    {
        return await _payrollRepository.GetPayrollAsync(month, year);
    }

    public async Task<PayslipDto?> GetPayslipAsync(int month, int year, int employeeId)
    {
        return await _payrollRepository.GetPayslipAsync(month, year, employeeId);
    }
}