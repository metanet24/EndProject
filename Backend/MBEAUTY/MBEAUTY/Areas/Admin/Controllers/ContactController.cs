using AutoMapper;
using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ContactVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var items = _mapper.Map<IEnumerable<ContactListVM>>(await _contactService.GetAllAsync());
            items = items.Skip((page * take) - take).Take(take);

            var paginateItems = new Paginate<ContactListVM>(items, page,
                await _contactService.GetPageCount(take));

            return View(paginateItems);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _contactService.GetByIdAsync((int)id);

            await _contactService.DeleteAsync(existItem);
            return Ok();
        }
    }
}
