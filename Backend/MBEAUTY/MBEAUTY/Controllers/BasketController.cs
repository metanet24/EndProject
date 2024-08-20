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
        private readonly UserManager<AppUser> _userManager;

        public BasketController(IBasketService basketService, UserManager<AppUser> userManager, IProductService productService)
        {
            _basketService = basketService;
            _userManager = userManager;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int? productId)
        {
            if (productId == null)
                return Json(new { redirectUrl = NotFound() });

            if (!User.Identity.IsAuthenticated)
                return Json(new { redirectUrl = Url.Action("SignIn", "Account") });

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            ProductDetailVM existProduct = await _productService.GetByIdAsync((int)productId);

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
    }
}
