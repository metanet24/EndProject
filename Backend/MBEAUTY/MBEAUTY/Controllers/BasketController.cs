using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BasketVMs;
using MBEAUTY.ViewModels.ProductVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(IBasketService basketService, UserManager<AppUser> userManager, IProductService productService, IMapper mapper)
        {
            _basketService = basketService;
            _userManager = userManager;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("SignIn", "Account");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            Basket basket = await _basketService.GetByAppUserIdAsync(existUser.Id);

            IEnumerable<BasketListVM> items = null;

            if (basket != null)
            {
                items = _mapper.Map<IEnumerable<BasketListVM>>(basket.BasketProducts);
            }

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int? productId)
        {
            if (productId == null)
                return Json(new { redirectUrl = NotFound() });

            if (!User.Identity.IsAuthenticated)
                return Json(new { redirectUrl = Url.Action("SignIn", "Account") });

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            ProductDetailVM existProduct = _mapper.Map<ProductDetailVM>(await _productService.GetByIdAsync((int)productId));

            if (existProduct == null)
                return Json(new { redirectUrl = Url.Action("NotFound", "Error") });

            if (await _basketService
                .BasketProductPlusByProductIdAndAppUserIdAsync((int)productId, existUser.Id))
                return Ok();

            if (await _basketService.AddBasketProductByAppUserIdAsync(existUser.Id, (int)productId))
                return Ok();

            var basketId = await _basketService.AddAsync(new BasketAddVM() { AppUserId = existUser.Id });

            await _basketService.AddBasketProductAsync(
                new BasketProductAddVM() { BasketId = basketId, ProductId = (int)productId });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Increase(int id)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Error");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            BasketProduct basketProduct = await _basketService.GetBasketProductByIdAsync((int)id);

            if (basketProduct == null)
                return RedirectToAction("NotFound", "Error");

            basketProduct.Quantity++;
            await _basketService.SaveAsync();

            decimal totalPrice = basketProduct.Product.Price * basketProduct.Quantity;
            decimal total = await _basketService.GetTotalByAppUserIdAsync(existUser.Id);

            return Ok(new { totalPrice, total });
        }

        [HttpPost]
        public async Task<IActionResult> Decrease(int id)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Error");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            BasketProduct basketProduct = await _basketService.GetBasketProductByIdAsync((int)id);

            if (basketProduct == null)
                return RedirectToAction("NotFound", "Error");

            if (basketProduct.Quantity > 1)
            {
                basketProduct.Quantity--;
            }
            else
            {
                await _basketService.DeleteBasketProduct(basketProduct);
            }
            await _basketService.SaveAsync();

            decimal totalPrice = basketProduct.Product.Price * basketProduct.Quantity;
            decimal total = await _basketService.GetTotalByAppUserIdAsync(existUser.Id);

            return Ok(new { totalPrice, total });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int? id, int? quantity)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Error");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            BasketProduct basketProduct = await _basketService.GetBasketProductByIdAsync((int)id);

            if (basketProduct == null)
                return RedirectToAction("NotFound", "Error");

            if (basketProduct.Quantity > 1)
            {
                basketProduct.Quantity = quantity ?? basketProduct.Quantity;
            }
            else
            {
                await _basketService.DeleteBasketProduct(basketProduct);
            }

            await _basketService.SaveAsync();

            decimal totalPrice = basketProduct.Product.Price * basketProduct.Quantity;
            decimal total = await _basketService.GetTotalByAppUserIdAsync(existUser.Id);
            int basketCount = await _basketService.GetCountByAppUserIdAsync(existUser.Id);

            return Ok(new { totalPrice, total, basketCount });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Error");

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("SignIn", "Account");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            BasketProduct basketProduct = await _basketService.GetBasketProductByIdAsync((int)id);

            if (basketProduct == null)
                return RedirectToAction("NotFound", "Error");

            await _basketService.DeleteBasketProduct(basketProduct);

            return Ok(await _basketService.GetTotalByAppUserIdAsync(existUser.Id));
        }
    }
}
