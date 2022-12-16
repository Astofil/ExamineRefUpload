using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class LocationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public LocationService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetLocation>>> GetLocation()
    {
        var list = await _context.Locations.Select(t => new GetLocation()
        {
            LocationId = t.LocationId,
            StreetAddress = t.StreetAddress,
            PostalCode = t.PostalCode,
            City = t.City,
            CountryId = t.CountryId,
            // CountryName = t.CountryName,
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetLocation>>(list);
    }

    public async Task<Response<AddLocation>> AddLocation(AddLocation location)
    {
        // var newLocation = new Location()
        // {
        //     LocationId = location.LocationId,
        //     StreetAddress = location.StreetAddress,
        //     PostalCode = location.PostalCode,
        //     City = location.City,
        //     CountryId = location.CountryId,
        //     // CountryName = location.CountryName,
        // };

        var newLocation = _mapper.Map<Location>(location);

        _context.Locations.Add(newLocation);
        await _context.SaveChangesAsync();
        return new Response<AddLocation>(location);
    }
    public async Task<Response<AddLocation>> UpdateLocation(AddLocation location)
    {
        var find = await _context.Locations.FindAsync(location.LocationId);
        find.LocationId = location.LocationId;
        find.StreetAddress = location.StreetAddress;
        find.PostalCode = location.PostalCode;
        find.City = location.City;
        find.CountryId = location.CountryId;
        // find.CountryName = location.CountryName;
        await _context.SaveChangesAsync();
        return new Response<AddLocation>(location);
    }
    public async Task<Response<string>> DeleteLocation(int id)
    {
        var find = await _context.Locations.FindAsync(id);
        _context.Locations.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Location succesfully deleted");
    }
}
