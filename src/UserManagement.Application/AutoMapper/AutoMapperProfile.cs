using AutoMapper;
using UserManagement.Application.DTOs.User;
using UserManagement.Domain.Models;

namespace UserManagement.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
    }
}