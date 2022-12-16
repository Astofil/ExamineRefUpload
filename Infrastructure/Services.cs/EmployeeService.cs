using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;
    public EmployeeService(DataContext context, IMapper mapper, IWebHostEnvironment environment)
    {
        _context = context;
        _mapper = mapper;
        _environment = environment;
    }
    public async Task<Response<List<GetEmployee>>> GetEmployee()
    {
        var list = await _context.Employees.Select(t => new GetEmployee()
        {
            EmployeeId = t.EmployeeId,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            PhoneNumber = t.PhoneNumber,
            HireDate = t.HireDate,
            Salary = t.Salary,
            CommissionPct = t.CommissionPct,
            Filename = t.FileName,
            JobId = t.JobId,
            JobTitle = t.Jobs.JobTitle,
            MinSalary = t.Jobs.MinSalary,
            MaxSalary = t.Jobs.MaxSalary,
            DepartmentId = t.DepartmentId,
            DepartmentName = t.Departments.DepartmentName,
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetEmployee>>(list);
    }

    public async Task<Response<AddEmployee>> AddEmployee(AddEmployee employee)
    {
        // var newEmployee = new Employee()
        // {
        //     EmployeeId = employee.EmployeeId,
        //     FirstName = employee.FirstName,
        //     LastName = employee.LastName,
        //     Email = employee.Email,
        //     PhoneNumber = employee.PhoneNumber,
        //     HireDate = employee.HireDate,
        //     Salary = employee.Salary,
        //     CommissionPct = employee.CommissionPct,
        //     JobId = employee.JobId,
        //     // JobTitle = employee.JobTitle,
        //     // MinSalary = employee.MinSalary,
        //     // MaxSalary = employee.MaxSalary,
        //     DepartmentId = employee.DepartmentId,
        //     // DepartmentName = employee.DepartmentName,
        // };

        var newEmployee = _mapper.Map<Employee>(employee);

        newEmployee.FileName = await UploadFile(employee.File);

        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync();
        return new Response<AddEmployee>(employee);
    }
    public async Task<Response<AddEmployee>> UpdateEmployee(AddEmployee employee)
    {
        var find = await _context.Employees.FindAsync(employee.EmployeeId);
        find.EmployeeId = employee.EmployeeId;
        find.FirstName = employee.FirstName;
        find.LastName = employee.LastName;
        find.Email = employee.Email;
        find.PhoneNumber = employee.PhoneNumber;
        find.HireDate = employee.HireDate;
        find.Salary = employee.Salary;
        find.CommissionPct = employee.CommissionPct;
        find.JobId = employee.JobId;
        find.ManagerId = employee.ManagerId;
        // find.JobTitle = employee.JobTitle;
        // find.MinSalary = employee.MinSalary;
        // find.MaxSalary = employee.MaxSalary;
        find.DepartmentId = employee.DepartmentId;
        // find.DepartmentName = employee.DepartmentName;

        if(employee.File != null)
        {
            find.FileName = await UpdateFile(employee.File, find.FileName);
        }

        await _context.SaveChangesAsync();
        return new Response<AddEmployee>(employee);
    }
    public async Task<Response<string>> DeleteEmployee(int id)
    {
        var find = await _context.Employees.FindAsync(id);
        _context.Employees.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Employee succesfully deleted");
    }

    private async Task<string> UploadFile(IFormFile file)
    {
        if(file == null) return null;

        var path = Path.Combine(_environment.WebRootPath, "employeefile");
        if(Directory.Exists(path) == false) Directory.CreateDirectory(path);
         
        var filepath = Path.Combine(path, file.FileName);
        using (var stream = new FileStream(filepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return file.FileName;
    }

    private async Task<string> UpdateFile(IFormFile file, string oldFileName)
    {
        var filepath = Path.Combine(_environment.WebRootPath, "employeefile", oldFileName);
        if(File.Exists(filepath) == true) File.Delete(filepath);

        var newFilepath = Path.Combine(_environment.WebRootPath, "employeefile", file.FileName);
        using (var stream = new FileStream(newFilepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return file.FileName;
    }
}
