using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace DataAccess.Mapping;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDto, AppUser>().ReverseMap();
        CreateMap<LoginRequestDto, AppUser>().ReverseMap();
    }
}
