using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApplication2.ViewModels;


namespace WebApplication2.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> Registration(ApplicationUser model)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email","Email is already taken");
                return RedirectToRoute(model);
            }

            user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,            
                DateTimeNow = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToRoute(new { area = "", controller = "Home", action = "Index" });
            }
            return RedirectToRoute(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> Login(ApplicationUser model)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null) 
            {
                return RedirectToRoute(model);
            }
                

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToRoute(new { area = "", controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("", "Wrong login and (or) password");
            }
            return RedirectToRoute(user);
        }

        public async Task<RedirectToRouteResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToRoute(new { area = "", controller = "Home", action = "Index" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl = null)
        {
            var redirect = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null, string returnUrl = null)
        {
            if (!String.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError(string.Empty, "Error from external provider");
                return View("Index");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Index");
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email});
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new ApplicationUser { FirstName = model.Name, UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "User");
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnUrl);
                    }
                }
                ModelState.AddModelError("Email", "User already exists");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            return View();
        }
    }
}
