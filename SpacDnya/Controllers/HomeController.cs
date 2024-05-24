using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacDnya.DAL;
using SpacDnya.ViewModels.Agencies;

namespace SpacDnya.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpacDnyaContext _context;

        public HomeController(SpacDnyaContext _context)
        {
            _context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Agencies
                .Select(c=> new GetAgencyVM
                {
                    Description = c.Description,
                    Id = c.Id,
                    Image= c.Image,
                    Name = c.Name,
                }).ToListAsync());
        }


    }
}
