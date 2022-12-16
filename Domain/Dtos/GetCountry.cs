using Domain.Entities;

namespace Domain.Dtos;

public class GetCountry
{
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public int RegionId { get; set; }
    public string RegionName { get; set; }
}
