using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace DataAccess.Mapping;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDto, AppUser>().ReverseMap();
    }
}
