using AutoMapper;
using BookStore.API.MapperModels.AuthorModels;
using BookStore.Domain.Models;

namespace BookStore.API.MapperConfiguration;

public class AuthorConfiguration : Profile
{
    public AuthorConfiguration()
    {
        CreateMap<AuthorCreateModel, Author>().ReverseMap();
        CreateMap<AuthorUpdateModel, Author>().ReverseMap();
        CreateMap<AuthorGetModel, Author>().ReverseMap();
    }
}