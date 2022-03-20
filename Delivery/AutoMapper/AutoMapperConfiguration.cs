using AutoMapper;
using Delivery.Core.ViewModels.Categories;
using Delivery.Infrastructure.Models;

namespace Delivery.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CategoryInputModel>().ReverseMap();
            CreateMap<Category, CategoryEditModel>().ReverseMap();
        }
    }
}