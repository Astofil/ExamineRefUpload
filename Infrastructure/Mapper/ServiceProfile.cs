using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<AddCountry, Country>();
        CreateMap<AddDepartment, Department>();
        CreateMap<AddEmployee, Employee>();
        CreateMap<AddJobGrade, JobGrade>();
        CreateMap<AddJobHistory, JobHistory>();
        CreateMap<AddJob, Job>();
        CreateMap<AddLocation, Location>();
        CreateMap<AddRegion, Region>();
    }
}