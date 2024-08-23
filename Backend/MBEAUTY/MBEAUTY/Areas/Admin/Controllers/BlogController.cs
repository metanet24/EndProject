using AutoMapper;
using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BlogVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6)
        {
            IEnumerable<BlogListVM> blogs = await _blogService.GetAllAsync();

            int pageCount = await _blogService.GetPageCount(take);

            Paginate<BlogListVM> result = new(blogs, page, pageCount);

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
