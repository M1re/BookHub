using BookStore.API.Data;
using AutoMapper;
using BookStore.API.Mapper.AuthorModels;

namespace BookStore.API.MapperConfiguration
{
    public class AuthorConfiguration : Profile
    {
        public AuthorConfiguration()
        {
            CreateMap<AuthorCreateModel,Author>().ReverseMap();
            CreateMap<AuthorUpdateModel,Author>().ReverseMap();
            CreateMap<AuthorGetModel,Author>().ReverseMap();
        }
    }
}
