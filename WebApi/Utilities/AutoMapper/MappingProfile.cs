using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<BookDtoForInsertion, Book>().ReverseMap();
            CreateMap<BookDtoForInsertion, BookDto>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>().ReverseMap();

            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
            CreateMap<CategoryDtoForInsertion, Category>().ReverseMap();
        }
    }
}