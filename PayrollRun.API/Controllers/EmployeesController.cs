using Microsoft.AspNetCore.Mvc;
using PayrollRun.API.Repositories.Interfaces;
using PayrollRun.API.Services.Interfaces;

namespace PayrollRun.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _repo;

    public EmployeesController(IEmployeeService repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var data = await _repo.GetEmployees();
        return Ok(data);
    }
}