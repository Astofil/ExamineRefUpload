namespace Domain.Entities;

public class JobHistory
{
    public int JobHistoryId { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employees { get; set; }
    public DateTime StartDate { get;set; }
    public DateTime EndDate { get;set; }

    public int JobId { get; set; }
    public Job Jobs { get; set; }

    public int DepartmentId { get; set; }
    public Department Departments { get; set; }
}

