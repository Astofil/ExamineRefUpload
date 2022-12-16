namespace Domain.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int ManagerId { get; set; }
    public Employee Managers { get; set; }


    public int LocationId { get; set; }
    public Location Locations { get; set; }

    public virtual List<Employee> Employees { get; set; }
    public virtual List<JobHistory> JobHistories { get; set; }
}
