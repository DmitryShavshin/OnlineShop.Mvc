using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using System.ComponentModel.DataAnnotations;
using WebApplication2.ViewModels;

namespace WebApplication2.Areas.Account.Controllers
{
    [Authorize]
    [Area("Account")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUser _user;
        private readonly IOrder _order;
        public UserController(UserManager<ApplicationUser> userManager, IUser user, IOrder order)
        {
            _userManager = userManager;
            _user = user;
            _order = order;
        }
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var user = await _userManager.Users
                    .Include(wa => wa.WorkAddress)
                    .Include(ha => ha.HomeAddress)
                    .Include(c => c.UserContact)
                    .FirstOrDefaultAsync(u => u.UserName == userName);
                return View(user);
            }

            return RedirectToRoute(new { area = "Account", controller = "Account", action = "Index" });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userManager.Users
                    .Include(wa => wa.WorkAddress)
                    .Include(ha => ha.HomeAddress)
                    .Include(c => c.UserContact)
                    .FirstOrDefaultAsync(u => u.Id == id);
            if (user != null) { 
                return View(user);
            }
            return NotFound();
        }

        public async Task<IActionResult> EditPhoneNumberPartial(Guid id, string phoneNumber)
        {
            var user = await _user.GetAllUserData(id);
            if (user != null)
            {
                var phoneNumberIsTaken = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
                if (phoneNumberIsTaken == null)
                {
                    var result = await _user.CheckingPhoneNumber(user, phoneNumber);
                    return View("Edit", result);
                }
                else
                {
                    ModelState.AddModelError("Phone", "Phone number is already taken");
                    return View("Edit", user);
                }
            }
            return View("Edit", user);
        }

        public async Task<IActionResult> EditEmailPartial(Guid id, string email)
        {
            var user = await _user.GetAllUserData(id);
            if (user != null)
            {
                var emailIsTaken = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (emailIsTaken == null)
                {
                    var result = await _user.CheckingEmail(user, email);
                    return View("Edit", result);
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already taken");
                    return View("Edit", user);
                }
            }
            return View("Edit", user);
        }

        public async Task<IActionResult> EditAvatarPartial( Guid id, IFormFile UserAvatar)
        {
            var user = await _user.GetAllUserData(id);
            if (user != null)
            {
                var result = _user.AddUserAvatar(id, UserAvatar);
                return View("Edit", result);
            }
            return View(user);
        }

        public async Task<IActionResult> EditUserInformationPartial(Guid id, ApplicationUser model)
        {
            var user = await _user.GetAllUserData(id);
            if (user != null || model != null || user != model)
            {
                var result = await _user.CheckingUserInformation(user, model);
                return View("Edit", result);
            }
            return View("Edit", model);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkPlacePartial(ApplicationUser model)
        {
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkPlacePartial(Guid id, ApplicationUser model, string ReturnUrl)
        {
            var user = await _user.GetAllUserData(id);
            if (user != null)
            {
                var result = await _user.EditUserWorkAddress(user, model);
                return View("Edit", result);
            }

            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(Guid id)
        {
            OrderDetailsViewModel orderModel = new OrderDetailsViewModel()
            {
                Order = await _order.GetUserOrders(id)
            };
           
            return View(orderModel);
        }
    }
}
