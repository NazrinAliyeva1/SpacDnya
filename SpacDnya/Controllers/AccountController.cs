using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpacDnya.DAL;
using SpacDnya.Models;
using SpacDnya.ViewModels.Account;

namespace SpacDnya.Controllers
{
    public class AccountController(UserManager<AppUser>_userManager, SignInManager<AppUser>_signInManager, SpacDnyaContext _context):Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new AppUser
            {
                Name = vm.Name,
                Email = vm.Email,
                Surname = vm.Surname,
                UserName = vm.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser? user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Istifadeci adi ve ya sifresi yanlisdir.");
                    return View(vm);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Cox sayda yanlis deyer gondermisiniz. Zehmet olmasa gozleyin --" + user.LockoutEnd.Value.ToString("HH:mm:ss"));
                return  View(vm);
            }
            return RedirectToAction("Index", "Home");
        }
         
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
