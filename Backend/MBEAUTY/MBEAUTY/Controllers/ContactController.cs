using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
