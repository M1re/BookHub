using AutoMapper;
using BookStore.Domain.Models;
using BookStoreAPI.MapperModels.UserModels;

namespace BookStoreAPI.MapperConfiguration
{
    public class UserConfiguration:Profile
    {
        public UserConfiguration()
        {
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
