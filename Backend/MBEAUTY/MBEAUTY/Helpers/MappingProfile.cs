using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.ViewModels.AboutVms;
using MBEAUTY.ViewModels.AdvertVMs;
using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.CategoryVMs;
using MBEAUTY.ViewModels.FamousVms;
using MBEAUTY.ViewModels.ProductVMs;
using MBEAUTY.ViewModels.ServicesVMs;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderListVM>();

            CreateMap<Banner, BannerVM>();

            CreateMap<Service, ServiceListVM>();

            CreateMap<Blog, BlogListVM>();

            CreateMap<About, AboutVM>();

            CreateMap<Famous, FamousListVM>();

            CreateMap<Brand, BrandListVM>();

            CreateMap<Advert, AdvertVM>();

            CreateMap<Category, CategoryListVM>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count()));

            CreateMap<Product, ProductListVM>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
