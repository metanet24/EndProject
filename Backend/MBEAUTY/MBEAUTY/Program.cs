using MBEAUTY.Data;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Implementations;
using MBEAUTY.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequiredLength = 8;
    option.Password.RequireUppercase = false;

    option.User.RequireUniqueEmail = true;

    option.SignIn.RequireConfirmedEmail = true;

    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.AllowedForNewUsers = true;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogImageService, BlogImageService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IFamousService, FamousService>();
builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IAdditionalInfoService, AdditionalInfoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
    developerExceptionPageOptions.SourceCodeLineCount = 1;
    app.UseDeveloperExceptionPage(developerExceptionPageOptions);

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
