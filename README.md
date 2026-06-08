# PAYROLL RUN SYSTEM – COMPLETE README

## PROJECT TITLE

**Payroll Run System**

---

## PROJECT DESCRIPTION

The Payroll Run System is a full-stack web application designed to automate employee salary calculation based on attendance data. It eliminates manual payroll processing and ensures accurate and efficient salary generation for each employee every month.

The system also includes **JWT Token-Based Authentication and Authorization** to secure API access and protect sensitive payroll operations.

---

## OBJECTIVE

The main objective of this project is to:

* Automate payroll calculation process
* Reduce manual salary errors
* Manage employee attendance and salary data
* Generate monthly payroll reports efficiently
* Secure APIs using JWT Authentication
* Restrict access based on user roles

---

## SYSTEM ARCHITECTURE

The system follows a layered architecture:

```text
Frontend (HTML, CSS, JavaScript)
        ↓
ASP.NET Core Web API
        ↓
Authentication & Authorization Layer
        ↓
Service Layer
        ↓
Repository Layer (Dapper ORM)
        ↓
SQL Server Database
```

---

## TECHNOLOGY STACK

### Backend

* ASP.NET Core Web API
* C#
* Dapper ORM
* Entity Framework Core(Authe)
* JWT Authentication
* Role-Based Authorization

### Database

* Microsoft SQL Server

### Frontend

* HTML5
* CSS3
* JavaScript (Fetch API)

---

## DATABASE TABLES

### 1. Employees

Stores employee information such as name and basic salary.

### 2. Attendance

Stores daily attendance records of employees (Present / Absent).

### 3. PayrollRuns

Stores payroll execution details for each month and year.

### 4. PayrollDetails

Stores calculated salary details for each employee.

### 5. Users

Stores application users for authentication and authorization.

Fields:

* {
  "userName": "string",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "role": "string"
}

---

## SYSTEM WORKFLOW

### Step 1

Employee attendance is recorded daily.

### Step 2

Authorized Admin logs into the system.

### Step 3

Admin triggers payroll run.

### Step 4

Stored procedure calculates salary based on attendance.

### Step 5

Payroll data is stored in PayrollDetails table.

### Step 6

Frontend fetches and displays payroll data.

### Step 7

Users access protected APIs using JWT Token.

---

## AUTHENTICATION & AUTHORIZATION

The application uses **JWT (JSON Web Token)** authentication.

### Authentication Features

* User Registration
* User Login
* JWT Token Generation
* Protected APIs
* Role-Based Authorization
* Secure Access Control


## SALARY CALCULATION LOGIC

```text
Daily Salary = Basic Salary / Working Days

Gross Pay = Daily Salary × Days Present

PF Deduction = 12% of Basic Salary

Professional Tax = Fixed 200

Net Pay = Gross Pay - PF Deduction - Professional Tax
```

---

## API ENDPOINTS

### Authentication APIs

#### Register User

```http
POST /api/auth/register
```

#### Login User

```http
POST /api/auth/login
```

#### Logout User

```http
POST /api/auth/logout
```

#### User Protected Endpoint

```http
GET /api/auth/user
```

Authorization Required:

```text
User Role
```

#### Admin Protected Endpoint

```http
GET /api/auth/admin
```

Authorization Required:

```text
Admin Role
```

---

### Payroll APIs

#### Run Payroll

```http
POST /api/payroll/run
```

#### Get Payroll Data

```http
GET /api/payroll?month=&year=
```

#### Get Payslip

```http
GET /api/payroll/payslip/{runId}/{employeeId}
```

---

### Employee APIs

#### Get All Employees

```http
GET /api/employees
```




## KEY DESIGN DECISIONS

* Dapper ORM used for high-performance data access
* Stored Procedures used for centralized payroll logic
* Layered Architecture implemented for maintainability
* Separation of Concerns followed for clean code structure
* JWT Authentication added for API security
* Role-Based Authorization implemented for access control

---

## SECURITY FEATURES

* JWT Token Authentication
* Password Hashing
* Protected API Endpoints
* Role-Based Authorization
* Secure Database Access
* Token Validation Middleware

---

## LIMITATIONS

* Attendance is manually inserted
* No email notifications
* No cloud deployment implemented
* No PDF payslip generation

---

## FEATURES NOT INCLUDED

To keep the project simple:

* Email Notifications
* Advanced Dashboard Analytics
* Biometric Attendance
* Docker Deployment
* Multi-Tenant Support

---

## FUTURE ENHANCEMENTS

* Employee Login Portal
* PDF Payslip Generation
* Email Salary Slip Feature
* Biometric Attendance Integration
* Advanced Analytics Dashboard
* Docker Containerization
* Cloud Deployment (Azure/AWS)
* Refresh Token Implementation

---

## HOW TO RUN PROJECT

### Backend Setup

```bash
dotnet restore
dotnet build
dotnet run
```

Application runs at:

```text
http://localhost:5288
```

---

### Frontend Setup

Open:

```text
index.html
```

OR use Live Server:

```text
http://127.0.0.1:5500
```

---

## IMPORTANT NOTES

* Ensure SQL Server is running
* Run database scripts before starting API
* Update connection string in appsettings.json
* Configure JWT Settings in appsettings.json
* Enable CORS for frontend communication
* Use JWT Token for protected APIs

---

## AUTHOR

**Nilesh kumbhar**

.NET Full Stack Developer

---

## PROJECT SUMMARY

The Payroll Run System is a payroll automation solution built using ASP.NET Core Web API, SQL Server, Dapper ORM, HTML, CSS, and JavaScript. The application automates salary processing, attendance tracking, payroll generation, and payslip management while securing APIs using JWT Authentication and Role-Based Authorization.
