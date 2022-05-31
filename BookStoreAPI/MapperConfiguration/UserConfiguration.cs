using AutoMapper;
using BookStore.API.MapperModels.UserModels;
using BookStore.Domain.Models;

namespace BookStore.API.MapperConfiguration;

public class UserConfiguration : Profile
{
    public UserConfiguration()
    {
        CreateMap<ApiUser, UserDTO>().ReverseMap();
    }
}