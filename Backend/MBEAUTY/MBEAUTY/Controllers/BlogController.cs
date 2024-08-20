using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BlogVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            IEnumerable<BlogListVM> blogs = await _blogService.GetAllAsync();
            blogs = blogs.Skip((page * take) - take).Take(take);

            Paginate<BlogListVM> paginateBlogs =
                new(blogs, page, await _blogService.GetPageCount(take));

            return View(paginateBlogs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            BlogDetailVM model = await _blogService.GetByIdAsync(id);
            model.Blogs = await _blogService.GetAllAsync();

            return View(model);
        }
    }
}
