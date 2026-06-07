 PAYROLL RUN SYSTEM – COMPLETE README
 PROJECT TITLE

Payroll Run System – Employee Salary Automation System

 PROJECT DESCRIPTION

The Payroll Run System is a full-stack web application designed to automate employee salary calculation based on attendance data. It eliminates manual payroll processing and ensures accurate and efficient salary generation for each employee every month.

 OBJECTIVE

The main objective of this project is to:

Automate payroll calculation process
Reduce manual salary errors
Manage employee attendance and salary data
Generate monthly payroll reports efficiently
 SYSTEM ARCHITECTURE

The system follows a layered architecture:

Frontend (HTML, CSS, JavaScript)
→ ASP.NET Core Web API
→ Service Layer
→ Repository Layer (Dapper ORM)
→ SQL Server Database

 TECHNOLOGY STACK
Backend
ASP.NET Core Web API
C#
Dapper ORM
Database
Microsoft SQL Server
Frontend
HTML5
CSS3
JavaScript (Fetch API)
 DATABASE TABLES
1. Employees

Stores employee information such as name and basic salary.

2. Attendance

Stores daily attendance records of employees (Present / Absent).

3. PayrollRuns

Stores payroll execution details for each month and year.

4. PayrollDetails

Stores calculated salary details for each employee.

 SYSTEM WORKFLOW

Step 1: Employee attendance is recorded daily
Step 2: Admin triggers payroll run
Step 3: Stored procedure calculates salary based on attendance
Step 4: Payroll data is stored in PayrollDetails table
Step 5: Frontend fetches and displays payroll data

 SALARY CALCULATION LOGIC

Daily Salary = Basic Salary / Working Days
Gross Pay = Daily Salary × Days Present
PF Deduction = 12% of Basic Salary
Professional Tax = Fixed 200
Net Pay = Gross Pay - PF - Professional Tax

 API ENDPOINTS
Payroll APIs

POST /api/payroll/run → Run payroll for a month
GET /api/payroll?month=&year= → Get payroll data
GET /api/payroll/payslip/{runId}/{employeeId} → Get payslip

Employee APIs

GET /api/employees → Get all employee details

 KEY DESIGN DECISIONS
Dapper ORM used for high performance and lightweight data access
Stored procedure used for centralized payroll logic
Layered architecture used for better maintainability
Separation of concerns implemented for clean code structure
 LIMITATIONS
 
No authentication system implemented
No role-based access control
Attendance is manually inserted
No cloud deployment implemented

 FEATURES NOT INCLUDED

To keep the project simple:

JWT Authentication
Email notifications
Advanced dashboards
DOCKER implemented

 FUTURE ENHANCEMENTS
Employee login system
PDF payslip generation
Email salary slip feature
Biometric attendance integration
Advanced analytics dashboard
 HOW TO RUN PROJECT
Backend Setup

dotnet restore
dotnet build
dotnet run

Application runs at:

http://localhost:5288

Frontend Setup

Open index.html in browser
OR use Live Server:

http://127.0.0.1:5500

 IMPORTANT NOTES
Ensure SQL Server is running
Run database scripts before starting API
Update connection string in appsettings.json
Enable CORS for frontend communication
 AUTHOR

Payroll Run System Project
Built using ASP.NET Core, Dapper, and SQL Server

 CONCLUSION

This project demonstrates a complete payroll automation system using modern web technologies. It provides accurate salary calculation based on attendance and ensures efficient payroll management.
