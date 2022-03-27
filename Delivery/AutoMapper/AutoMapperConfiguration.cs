using AutoMapper;
using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Extras;
using Delivery.Core.ViewModels.Packagies;
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
            CreateMap<Package, PackageInputModel>().ReverseMap();
            CreateMap<Package, PackageEditModel>().ReverseMap();
            CreateMap<Package, PackageViewModel>().ReverseMap();
            CreateMap<Extra, ExtraInpitModel>().ReverseMap();
            CreateMap<Extra, ExtraEditModel>().ReverseMap();
            CreateMap<Extra, ExtraViewModel>().ReverseMap();
        }
    }
}