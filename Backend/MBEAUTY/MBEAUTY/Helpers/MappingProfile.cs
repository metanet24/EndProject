﻿using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
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

            CreateMap<Brand, BrandListVM>();

            CreateMap<Banner, BannerVM>();

            CreateMap<Service, ServiceListVM>();

            CreateMap<Blog, BlogListVM>();

            CreateMap<Product, ProductListVM>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
