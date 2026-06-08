using Microsoft.AspNetCore.Mvc;
using PayrollRun.API.DTOs;
using PayrollRun.API.Services.Interfaces;

namespace PayrollRun.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayrollController : ControllerBase
{
    private readonly IPayrollService _payrollService;

    public PayrollController(IPayrollService payrollService)
    {
        _payrollService = payrollService;
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunPayroll(
        [FromBody] PayrollRunRequest request)
    {
        try
        {
            await _payrollService.RunPayrollAsync(
                request.Month,
                request.Year);

            return Ok(new
            {
                Success = true,
                Message = "Payroll processed successfully."
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPayroll(
        [FromQuery] int month,
        [FromQuery] int year)
    {
        var result = await _payrollService.GetPayrollAsync(
            month,
            year);

        return Ok(result);
    }

    [HttpGet("payslip/{month:int}/{year:int}/{employeeId:int}")]
    public async Task<IActionResult> GetPayslip(
    int month,
    int year,
    int employeeId)
    {
        var result = await _payrollService.GetPayslipAsync(
            month,
            year,
            employeeId);

        if (result == null)
            return NotFound("Payslip not found for given period");

        return Ok(result);
    }
}