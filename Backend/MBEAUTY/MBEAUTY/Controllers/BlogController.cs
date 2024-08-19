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

        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

        public async Task<IActionResult> Detail(int id)
        {
            BlogDetailVM model = await _blogService.GetByIdAsync(id);
            model.Blogs = await _blogService.GetAllAsync();

            return View(model);
        }
    }
}
