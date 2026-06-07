const apiUrl = "http://localhost:5288/api";

async function runPayroll() {

    const month = document.getElementById("month").value;
    const year = document.getElementById("year").value;

    try {

        document.getElementById("message").innerText =
            "Running Payroll...";

        const response = await fetch(
            `${apiUrl}/payroll/run`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    month: Number(month),
                    year: Number(year)
                })
            }
        );

        const result = await response.json();

        if (!response.ok) {
            throw new Error(result.message);
        }

        document.getElementById("message").innerText =
            result.message;

    }
    catch (error) {

        document.getElementById("message").innerText =
            error.message || "Error Running Payroll";
    }
}

async function getPayroll() {

    const month = document.getElementById("month").value;
    const year = document.getElementById("year").value;

    try {

        const response = await fetch(
            `${apiUrl}/payroll?month=${month}&year=${year}`
        );

        if (!response.ok) {
            throw new Error("Payroll data not found");
        }

        const data = await response.json();

        const tbody =
            document.querySelector("#payrollTable tbody");

        tbody.innerHTML = "";

        if (data.length === 0) {

            document.getElementById("message").innerText =
                "No payroll records found";

            return;
        }

        data.forEach(emp => {

            tbody.innerHTML += `
            <tr>
                <td>${emp.employeeName}</td>
                <td>${emp.basicSalary}</td>
                <td>${emp.workingDays}</td>
                <td>${emp.daysPresent}</td>
                <td>${emp.grossPay}</td>
                <td>${emp.pfDeduction}</td>
                <td>${emp.professionalTax}</td>
                <td>${emp.netPay}</td>
            </tr>`;
        });

        document.getElementById("message").innerText =
            "Payroll loaded successfully";
    }
    catch (error) {

        document.getElementById("message").innerText =
            error.message;
    }
}