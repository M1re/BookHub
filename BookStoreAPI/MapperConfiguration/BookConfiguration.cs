using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Mapper.BookModels;

namespace BookStore.API.MapperConfiguration
{
    public class BookConfiguration:Profile
    {
        public BookConfiguration()
        {
            CreateMap<Book,BookGetModel>()
                .ForMember(a => a.AuthorName, b => b.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<BookGetModel, Book>().ReverseMap();
            CreateMap<BookCreateModel, Book>().ReverseMap();
            CreateMap<BookUpdateModel, Book>().ReverseMap();

        }
    }
}
