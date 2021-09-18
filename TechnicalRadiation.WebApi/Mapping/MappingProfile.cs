using System.Net;
using AutoMapper;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;


namespace TechnicalRadiation.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsItem, NewsItemDetailDto>();
            CreateMap<NewsItem, NewsItemDto>();
            CreateMap<NewsItemInputModel, NewsItem>();
            CreateMap<Author, AuthorDetailDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorInputModel, Author>();
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryInputModel, Category>();
        }
    }
}