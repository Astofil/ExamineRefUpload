using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public DepartmentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetDepartment>>> GetDepartment()
    {
        var list = await _context.Departments.Select(t => new GetDepartment()
        {
            DepartmentId = t.DepartmentId,
            DepartmentName = t.DepartmentName,
            LocationId = t.LocationId,
            ManagerId = t.ManagerId,
            StreetAddress = t.Locations.StreetAddress,
            PostalCode  = t.Locations.PostalCode,
            City = t.Locations.City
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetDepartment>>(list);
    }

    public async Task<Response<AddDepartment>> AddDepartment(AddDepartment department)
    {
        // var newDepartment = new Department()
        // {
        //     DepartmentId = department.DepartmentId,
        //     DepartmentName = department.DepartmentName,
        //     ManagerId = department.ManagerId,
        //     LocationId = department.LocationId,
        // };

        var newDepartment = _mapper.Map<Department>(department);

        _context.Departments.Add(newDepartment);
        await _context.SaveChangesAsync();
        return new Response<AddDepartment>(department);
    }
    public async Task<Response<AddDepartment>> UpdateDepartment(AddDepartment department)
    {
        var find = await _context.Departments.FindAsync(department.DepartmentId);
        find.DepartmentId = department.DepartmentId;
        find.DepartmentName = department.DepartmentName;
        find.ManagerId = department.ManagerId;
        find.LocationId = department.LocationId;
        await _context.SaveChangesAsync();
        return new Response<AddDepartment>(department);
    }
    public async Task<Response<string>> DeleteDepartment(int id)
    {
        var find = await _context.Departments.FindAsync(id);
        _context.Departments.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Department succesfully deleted");
    }
}
