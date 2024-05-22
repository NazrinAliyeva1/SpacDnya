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
                Id = p.Id,
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

    public async Task<IActionResult> Update(int? id)
    {
        if(id==null || id < 1) return BadRequest();
        Agency agency = await _context.Agencies.FirstOrDefaultAsync(a=>a.Id==id);
        if (agency is null) return NotFound();

        UpdateAgencyVM updateVM = new UpdateAgencyVM
        {
            Description = agency.Description,
            Image = agency.Image,
            Name = agency.Name,

        };
        return View(updateVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, UpdateAgencyVM updateVM)
    {
        if (id == null || id < 1) return BadRequest();
        Agency existed = await _context.Agencies.FirstOrDefaultAsync(b=>b.Id==id);
        if (existed is null) return NotFound();
        
        existed.Name= updateVM.Name;
        existed.Description= updateVM.Description;
        existed.Image= updateVM.Image;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}
