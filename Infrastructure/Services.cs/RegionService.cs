using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class RegionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public RegionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetRegion>>> GetRegion()
    {
        var list = await _context.Regions.Select(t => new GetRegion()
        {
            RegionId = t.RegionId,
            RegionName = t.RegionName
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetRegion>>(list);
    }

    public async Task<Response<AddRegion>> AddRegion(AddRegion region)
    {
        // var newRegion = new Region()
        // {
        //     RegionId = region.RegionId,
        //     RegionName = region.RegionName
        // };

        var newRegion = _mapper.Map<Region>(region);

        _context.Regions.Add(newRegion);
        await _context.SaveChangesAsync();
        return new Response<AddRegion>(region);
    }
    public async Task<Response<AddRegion>> UpdateRegion(AddRegion region)
    {
        var find = await _context.Regions.FindAsync(region.RegionId);
        find.RegionId = region.RegionId;
        find.RegionName = region.RegionName;
        await _context.SaveChangesAsync();
        return new Response<AddRegion>(region);
    }
    public async Task<Response<string>> DeleteRegion(int id)
    {
        var find = await _context.Regions.FindAsync(id);
        _context.Regions.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Region succesfully deleted");
    }
}
