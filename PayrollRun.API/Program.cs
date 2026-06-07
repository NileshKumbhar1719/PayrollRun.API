using PayrollRun.API.Data;
using PayrollRun.API.Repositories;
using PayrollRun.API.Repositories.Interfaces;
using PayrollRun.API.Services;
using PayrollRun.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DapperContext>();

// Repository registrations
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();

// Service registrations

builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                 .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();