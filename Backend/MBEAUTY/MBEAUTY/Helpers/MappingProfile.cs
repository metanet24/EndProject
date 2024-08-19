using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.ViewModels.AboutVms;
using MBEAUTY.ViewModels.AccountVMs;
using MBEAUTY.ViewModels.AdvertVMs;
using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.CategoryVMs;
using MBEAUTY.ViewModels.ContactVMs;
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

            CreateMap<About, AboutVM>();

            CreateMap<Famous, FamousListVM>();

            CreateMap<Brand, BrandListVM>();

            CreateMap<Advert, AdvertVM>();

            CreateMap<ContactAddVM, Contact>();

            CreateMap<SignUpVM, AppUser>();

            CreateMap<Blog, BlogListVM>();
            CreateMap<Blog, BlogDetailVM>();

            CreateMap<Category, CategoryListVM>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count()));

            CreateMap<Product, ProductListVM>();
            CreateMap<Product, ProductDetailVM>()
                .ForMember(dest => dest.SkinType, opt => opt.MapFrom(src => src.AdditionalInfo.SkinType))
                .ForMember(dest => dest.Shades, opt => opt.MapFrom(src => src.AdditionalInfo.Shades));
        }
    }
}
