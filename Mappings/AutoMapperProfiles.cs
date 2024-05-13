using AutoMapper;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // For Regions
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionDto, Region>().ReverseMap();
        CreateMap<UpdateRegionDto, Region>().ReverseMap();

        // For Difficulty
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<AddDifficultyDto, Difficulty>().ReverseMap();
        CreateMap<UpdateDifficultyDto, Difficulty>().ReverseMap();

        // For Walk
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<AddWalkDto, Walk>().ReverseMap();
        CreateMap<UpdateWalkDto, Walk>().ReverseMap();
    }    
}