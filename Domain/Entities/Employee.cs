using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int JobId { get; set; }
    public Job Jobs { get; set; }
    public int Salary { get; set; }
    public int CommissionPct { get; set; }
    public string FileName { get; set; }

    public int ManagerId { get; set; }

    public int DepartmentId { get; set; }
    public Department Departments { get; set; }
    public List<JobHistory> JobHistories { get; set; }
    
}
