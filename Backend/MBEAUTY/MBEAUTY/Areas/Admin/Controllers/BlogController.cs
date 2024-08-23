using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BlogVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogImageService _blogImageService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper, IBlogImageService blogImageService)
        {
            _blogService = blogService;
            _mapper = mapper;
            _blogImageService = blogImageService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6)
        {
            IEnumerable<BlogListVM> blogs = _mapper.Map<IEnumerable<BlogListVM>>(
                await _blogService.GetAllAsync());

            int pageCount = await _blogService.GetPageCount(take);

            Paginate<BlogListVM> result = new(blogs, page, pageCount);

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Blog> products = await _blogService.GetAllAsync();

            if (products.Any(m => m.Name == request.Name))
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View(request);
            }

            foreach (var photo in request.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "Please, choose correct image type");
                    return View(request);
                }

                if (!photo.CheckFileSize(10000))
                {
                    ModelState.AddModelError("Photos", "Please, choose correct image size");
                    return View(request);
                }
            }

            var blogId = await _blogService.AddAsync(request);

            await _blogImageService.AddAsync(blogId, request.Photos);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            BlogDetailVM model = _mapper.Map<BlogDetailVM>(await _blogService.GetByIdAsync((int)id));

            if (model == null) return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Blog existItem = await _blogService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<BlogEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Blog existItem = await _blogService.GetByIdAsync(id);

            ICollection<BlogImage> blogImages = null;

            if (request.Photos != null)
            {
                if (existItem.BlogImages.Count() + request.Photos.Count() > 5)
                {
                    ModelState.AddModelError("Photos", "There should be a maximum of 5 images");
                    request.BlogImages = existItem.BlogImages;
                    return View(request);
                }

                foreach (var photo in request.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "Please, choose correct image type");
                        request.BlogImages = existItem.BlogImages;
                        return View(request);
                    }

                    if (!photo.CheckFileSize(10000))
                    {
                        ModelState.AddModelError("Photos", "Please, choose correct image size");
                        request.BlogImages = existItem.BlogImages;
                        return View(request);
                    }
                }

                blogImages = await _blogImageService.AddAsync(existItem.Id, request.Photos);
            }

            request.BlogImages = request.Photos == null ? existItem.BlogImages : blogImages;
            await _blogService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _blogImageService.GetByIdAsync((int)id);

            if (existItem.Blog.BlogImages.Count == 1 || existItem.IsMain) return Ok(false);

            await _blogImageService.DeleteAsync(existItem);
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> EditImageType(int? id)
        {
            if (id == null) return BadRequest();
            await _blogImageService.UpdateType((int)id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _blogService.GetByIdAsync((int)id);

            foreach (var item in existItem.BlogImages)
            {
                await _blogImageService.DeleteAsync(item);
            }

            await _blogService.DeleteAsync(existItem);
            return Ok();
        }
    }
}
