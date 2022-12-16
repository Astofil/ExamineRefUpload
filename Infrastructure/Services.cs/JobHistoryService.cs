using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class JobHistoryService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public JobHistoryService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetJobHistory>>> GetJobHistory()
    {
        var list = await _context.JobHistories.Select(t => new GetJobHistory()
        {
            EmployeeId = t.EmployeeId,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            JobId = t.JobId,
            JobTitle = t.Jobs.JobTitle,
            MinSalary = t.Jobs.MinSalary,
            MaxSalary = t.Jobs.MaxSalary,
            DepartmentId = t.DepartmentId,
            DepartmentName = t.Departments.DepartmentName,
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetJobHistory>>(list);
    }

    public async Task<Response<AddJobHistory>> AddJobHistory(AddJobHistory jobHistory)
    {
        // var newJobHistory = new JobHistory()
        // {
        //     EmployeeId = jobHistory.EmployeeId,
        //     StartDate = jobHistory.StartDate,
        //     EndDate = jobHistory.EndDate,
        //     JobId = jobHistory.JobId,
        //     DepartmentId = jobHistory.DepartmentId,
        // };

        var newJobHistory = _mapper.Map<JobHistory>(jobHistory);

        _context.JobHistories.Add(newJobHistory);
        await _context.SaveChangesAsync();
        return new Response<AddJobHistory>(jobHistory);
    }
    public async Task<Response<AddJobHistory>> UpdateJobHistory(AddJobHistory jobHistory)
    {
        var find = await _context.JobHistories.FindAsync(jobHistory.EmployeeId);
        find.EmployeeId = jobHistory.EmployeeId;
        find.StartDate = jobHistory.StartDate;
        find.EndDate = jobHistory.EndDate;
        find.JobId = jobHistory.JobId;
        find.DepartmentId = jobHistory.DepartmentId;
        await _context.SaveChangesAsync();
        return new Response<AddJobHistory>(jobHistory);
    }
    public async Task<Response<string>> DeleteJobHistory(int id)
    {
        var find = await _context.JobHistories.FindAsync(id);
        _context.JobHistories.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("JobHistory succesfully deleted");
    }
}
