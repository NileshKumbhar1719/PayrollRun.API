using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using PayrollRun.API.Controllers;
using PayrollRun.API.DTOs;
using PayrollRun.API.Services.Interfaces;
using System.Threading.Tasks;

namespace PayrollRun.Tests
{
    public class PayrollControllerTests
    {
        private Mock<IPayrollService> _mockService;
        private PayrollController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPayrollService>();
            _controller = new PayrollController(_mockService.Object);
        }

        [Test]
        public async Task GetPayslip_ReturnsOk_WhenDataExists()
        {
            int runId = 1;
            int employeeId = 2;

            var mockResult = new PayslipDto
            {
                EmployeeId = employeeId,
                EmployeeName = "Amit Kumar",
                BasicSalary = 35000,
                WorkingDays = 26,
                DaysPresent = 21,
                GrossPay = 24000,
                PFDeduction = 4200,
                ProfessionalTax = 200,
                NetPay = 19600
            };

            _mockService
                .Setup(s => s.GetPayslipAsync(runId, employeeId))
                .ReturnsAsync(mockResult);

            var result = await _controller.GetPayslip(runId, employeeId);

           
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetPayslip_ReturnsNotFound_WhenDataIsNull()
        {
            _mockService
                .Setup(s => s.GetPayslipAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((PayslipDto)null);

            var result = await _controller.GetPayslip(1, 2);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}