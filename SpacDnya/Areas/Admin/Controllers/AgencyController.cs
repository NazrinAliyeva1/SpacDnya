using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacDnya.DAL;
using SpacDnya.Extentions;
using SpacDnya.Models;
using SpacDnya.ViewModels.Agencies;

namespace SpacDnya.Areas.Admin.Controllers;

[Area("Admin")]
public class AgencyController(SpacDnyaContext _context, IWebHostEnvironment _env) : Controller
{


    public async Task<IActionResult> Index()
    {

        return View(await _context.Agencies
            .Select(p=> new GetAgencyAdminVM
            {
                CreatedTime= p.CreateTime.ToString("dd MMM dddd"),
                UpdatedTime= p.UpdateTime.ToString("dd MMM dddd"),
                Description= p.Description,
                Image =p.Image,
                Name = p.Name,
            }).ToListAsync());  
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateAgencyAdminVM data)
    {

        if(data.Photo != null)
        {
            if (!data.Photo.IsValidType("image"))
            {
                ModelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                return View();
            }
            if (!data.Photo.IsValidLength(900))
            {
                ModelState.AddModelError("Photo", "Faylin hecmi 900-den cox olmamalidir");

                return View();
            }
        }
        if(!ModelState.IsValid)
        {
            return View();
        }
        string fileName = await data.Photo.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "agncs"));

        Agency agency = new Agency
        {
            Name = data.Name,
            Description = data.Description,
            CreateTime=DateTime.Now,
            UpdateTime= DateTime.Now,
            Image= Path.Combine("imgs", "agncs", fileName)
        };
        await _context.Agencies.AddAsync(agency);   
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
