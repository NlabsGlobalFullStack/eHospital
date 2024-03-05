using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace DataAccess.Mapping;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDto, AppUser>()
            .ForMember(member => member.FirstName, options => options.MapFrom(src => src.FirstName.Trim()))
            .ForMember(member => member.LastName, options => options.MapFrom(src => src.LastName.Trim()))
            .ForMember(member => member.Email, options =>
                                        options.MapFrom(src => src.Email!.ToLower().Trim()))
            .ForMember(member => member.FullAddress, options => options.MapFrom(src => src.FullAddress.Trim()))
            .ForMember(member => member.UserName, options =>
                        options.MapFrom(src => src.UserName == null
                                        ? src.FirstName
                                            .Trim()
                                            .ToLower()
                                            .Replace(" ", "")
                                            .Replace("ğ", "g")
                                            .Replace("ş", "s")
                                        : src.UserName.Trim().ToLower()
                                        ));

        CreateMap<CreatePatientDto, AppUser>()
        .ForMember(member => member.LastName, options => options.MapFrom(src => src.LastName.Trim()))
            .ForMember(member => member.Email, options =>
                                        options.MapFrom(src => src.Email!.ToLower().Trim()))
            .ForMember(member => member.FullAddress, options => options.MapFrom(src => src.FullAddress.Trim()))
            .ForMember(member => member.UserName, options =>
                        options.MapFrom(src =>
                                    src.FirstName
                                            .Trim()
                                            .ToLower()
                                            .Replace(" ", "")
                                            .Replace("ğ", "g")
                                            .Replace("ş", "s")));
        ;

        CreateMap<CreateAppointmentDto, Appointment>()
            .ForMember(member => member.StartDate, options =>
                        options.MapFrom(src => DateTime.SpecifyKind(src.StartDate, DateTimeKind.Utc)))
            .ForMember(member => member.EndDate, options =>
                        options.MapFrom(src => DateTime.SpecifyKind(src.EndDate, DateTimeKind.Utc)))
            ;
    }
}
