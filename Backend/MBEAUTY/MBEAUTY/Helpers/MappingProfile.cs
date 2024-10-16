﻿using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.ViewModels.AboutVms;
using MBEAUTY.ViewModels.AccountVMs;
using MBEAUTY.ViewModels.AdditionalInfoVMs;
using MBEAUTY.ViewModels.AdvertVMs;
using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BasketVMs;
using MBEAUTY.ViewModels.BlogImageVMs;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.CategoryVMs;
using MBEAUTY.ViewModels.ContactVMs;
using MBEAUTY.ViewModels.FamousVms;
using MBEAUTY.ViewModels.ProductImageVMs;
using MBEAUTY.ViewModels.ProductVMs;
using MBEAUTY.ViewModels.ServicesVMs;
using MBEAUTY.ViewModels.SettingVMs;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketProduct, BasketListVM>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.Product.ProductImages.FirstOrDefault(m => m.IsMain).Name))
                .ForMember(dest => dest.TotalPrice,
                opt => opt.MapFrom(src => src.Product.Price * src.Quantity));

            CreateMap<Setting, SettingListVM>();
            CreateMap<SettingAddVM, Setting>();
            CreateMap<Setting, SettingEditVM>();
            CreateMap<SettingEditVM, Setting>();

            CreateMap<Slider, SliderListVM>();
            CreateMap<SliderAddVM, Slider>();
            CreateMap<Slider, SliderEditVM>();
            CreateMap<SliderEditVM, Slider>();

            CreateMap<Banner, BannerVM>();
            CreateMap<BannerEditVM, Banner>();
            CreateMap<Banner, BannerEditVM>();

            CreateMap<Service, ServiceListVM>();
            CreateMap<ServiceAddVM, Service>();
            CreateMap<Service, ServiceEditVM>();
            CreateMap<ServiceEditVM, Service>();

            CreateMap<About, AboutVM>();
            CreateMap<About, AboutEditVM>();
            CreateMap<AboutEditVM, About>();

            CreateMap<Famous, FamousListVM>();
            CreateMap<FamousAddVM, Famous>();
            CreateMap<Famous, FamousEditVM>();
            CreateMap<FamousEditVM, Famous>();

            CreateMap<Brand, BrandListVM>();
            CreateMap<BrandAddVM, Brand>();
            CreateMap<Brand, BrandEditVM>();
            CreateMap<BrandEditVM, Brand>();

            CreateMap<Advert, AdvertVM>();
            CreateMap<AdvertAddVM, Advert>();
            CreateMap<Advert, AdvertEditVM>();
            CreateMap<AdvertEditVM, Advert>();

            CreateMap<ContactAddVM, Contact>();
            CreateMap<Contact, ContactListVM>();

            CreateMap<SignUpVM, AppUser>();

            CreateMap<BasketAddVM, Basket>();
            CreateMap<BasketProductAddVM, BasketProduct>();

            CreateMap<Blog, BlogListVM>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.BlogImages.FirstOrDefault(m => m.IsMain).Name));

            CreateMap<Blog, BlogDetailVM>();
            CreateMap<BlogAddVM, Blog>();
            CreateMap<Blog, BlogEditVM>();
            CreateMap<BlogEditVM, Blog>()
                .ForMember(dest => dest.BlogImages, opt => opt.Ignore());

            CreateMap<BlogImageAddVM, BlogImage>();

            CreateMap<ProductImageAddVM, ProductImage>();
            CreateMap<ProductImage, ProductImageDetailVM>();
            CreateMap<ProductImageDetailVM, ProductImage>();

            CreateMap<AdditionalInfo, AdditionalInfoDetailVM>();
            CreateMap<AdditionalInfoAddVM, AdditionalInfo>();
            CreateMap<AdditionalInfoDetailVM, AdditionalInfoEditVM>();
            CreateMap<AdditionalInfoEditVM, AdditionalInfo>();

            CreateMap<CategoryAddVM, Category>();
            CreateMap<Category, CategoryEditVM>();
            CreateMap<CategoryEditVM, Category>();
            CreateMap<Category, CategoryListVM>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count()));

            CreateMap<ProductAddVM, Product>();
            CreateMap<ProductEditVM, Product>()
                 .ForMember(dest => dest.ProductImages, opt => opt.Ignore());

            CreateMap<Product, ProductEditVM>()
                .ForMember(dest => dest.SkinType, opt => opt.MapFrom(src => src.AdditionalInfo.SkinType))
                .ForMember(dest => dest.Shades, opt => opt.MapFrom(src => src.AdditionalInfo.Shades));

            CreateMap<Product, ProductListVM>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(m => m.IsMain).Name));

            CreateMap<Product, ProductDetailVM>()
                .ForMember(dest => dest.SkinType, opt => opt.MapFrom(src => src.AdditionalInfo.SkinType))
                .ForMember(dest => dest.Shades, opt => opt.MapFrom(src => src.AdditionalInfo.Shades));
        }
    }
}
