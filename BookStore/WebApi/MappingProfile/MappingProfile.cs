using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.Entity;

namespace WebApi.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //ilk parametre source, ikincisi target
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())); 


        }
    }
}
