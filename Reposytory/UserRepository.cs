using Microsoft.AspNetCore.Identity;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Reposytory
{
    public class UserRepository : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHost;
        private readonly ApplicationDbContext _context;
        public UserRepository(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ApplicationDbContext context,
                                IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHost = webHost;
            _context = context;
        }

        public ApplicationUser GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == name);
        }
        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());          
        }

        public async Task<ApplicationUser> GetAllUserData(Guid id)
        {
            var user = await _userManager.Users
                .Include(c => c.UserContact)
                .Include(h => h.HomeAddress)
                .Include(w => w.WorkAddress)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }


        public ApplicationUser AddUserAvatar(Guid id, IFormFile file)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var userImg = UploadImage(file);
            
            if (user != null)
                user.ImgUrl = userImg;
                _context.Users.Update(user);
                
            _context.SaveChanges();
            return(user);
        }

        public async Task<ApplicationUser> EditPhoneNumber(Guid id, string phone)
        {
            var user = await GetUserById(id);
            if(user != null)
                user.PhoneNumber = phone;
            _context.Users.Update(user);
            _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> EditEmail(Guid id, string email)
        {
            var user = await GetUserById(id);
            if (user != null)
                user.Email = email;
            _context.Users.Update(user);
            _context.SaveChangesAsync();
            return user;
        }
        public Task<ApplicationUser> EditUserInformation()
        {
            throw new NotImplementedException();
        }

        private string UploadImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public async Task<ApplicationUser> CheckingPhoneNumber(ApplicationUser user, string phoneNumber)
        {
            ApplicationUser result = new ApplicationUser();
            if (phoneNumber != null && user.PhoneNumber != phoneNumber)
            {
                result = await EditPhoneNumber(user.Id, phoneNumber);
            }
            return result;
        }

        public async Task<ApplicationUser> CheckingEmail(ApplicationUser user, string email)
        {
            ApplicationUser result = new ApplicationUser();
            if (user.Email != email && email != null)
            {
                result = await EditEmail(user.Id, email);
            }
            return result;
        }

        public async Task<ApplicationUser> CheckingUserInformation(ApplicationUser user, ApplicationUser model)
        {
            ApplicationUser result = new ApplicationUser();

            if (model.UserContact != null)
                result = await EditUserContact(user, model);
            if (model.HomeAddress != null)
                result = await EditHomeAddress(user, model);
            if (model != null)
                result = await EditUser(user.Id, model);

            return (result);
        }

        public async Task<ApplicationUser> EditUser(Guid id, ApplicationUser model)
        {         
            var updateUser = await GetUserById(id);
            if (updateUser != null)
            {
                updateUser.FirstName = !String.IsNullOrEmpty(model.FirstName) ? model.FirstName : updateUser.FirstName;
                updateUser.LastName = !String.IsNullOrEmpty(model.LastName) ? model.LastName : updateUser.LastName;
                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
                return updateUser;
            }
            return model;
        }

        public async Task<ApplicationUser> EditHomeAddress(ApplicationUser user, ApplicationUser model)
        {
            var result = await _context.UserHomeAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);

            if (result != null)
            {
                UserHomeAddress userAddress = new UserHomeAddress()
                {
                    Country =  !String.IsNullOrEmpty(model.HomeAddress.Country) ? model.HomeAddress.Country : user.HomeAddress.Country,
                    City = !String.IsNullOrEmpty(model.HomeAddress.City) ? model.HomeAddress.City : user.HomeAddress.City,
                    Street = !String.IsNullOrEmpty(model.HomeAddress.Street) ? model.HomeAddress.Street : user.HomeAddress.Street,
                    PostalCode = !String.IsNullOrEmpty(model.HomeAddress.PostalCode.ToString()) ? model.HomeAddress.PostalCode : user.HomeAddress.PostalCode,
                    UserId = String.IsNullOrEmpty(result.UserId.ToString()) ? model.Id : result.UserId
                };
                _context.UserHomeAddresses.Update(userAddress);
                await _context.SaveChangesAsync();
                return user;
            }
            user = await AddUserHomeAddress(user, model);
            return user;
        }

        public async Task<ApplicationUser> AddUserHomeAddress(ApplicationUser user, ApplicationUser model)
        {
            UserHomeAddress userAddress = new UserHomeAddress()
            {
                Country = model.HomeAddress.Country,
                City = model.HomeAddress.City,
                Street = model.HomeAddress.Street,
                PostalCode = model.HomeAddress.PostalCode,
                UserId = user.Id
            };
            await _context.UserHomeAddresses.AddAsync(userAddress);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<ApplicationUser> EditUserContact(ApplicationUser user, ApplicationUser model)
        {
            var result = await _context.UserContacts.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (result != null)
            {
                UserContact userContact = new UserContact()
                {
                    Instagram = !String.IsNullOrEmpty(model.UserContact.Instagram) ? model.UserContact.Instagram : user.UserContact.Instagram,
                    Facebook = !String.IsNullOrEmpty(model.UserContact.Facebook) ? model.UserContact.Facebook : user.UserContact.Facebook,
                    Linkedin = !String.IsNullOrEmpty(model.UserContact.Linkedin) ? model.UserContact.Linkedin : user.UserContact.Linkedin,
                    Gender = !String.IsNullOrEmpty(model.UserContact.Gender) ? model.UserContact.Gender : user.UserContact.Gender,
                    BirthDate = !String.IsNullOrEmpty(model.UserContact.BirthDate) ? model.UserContact.BirthDate : user.UserContact.BirthDate,
                    UserId = String.IsNullOrEmpty(result.UserId.ToString()) ? model.Id : result.UserId,
                };
                _context.UserContacts.Update(userContact);
                await _context.SaveChangesAsync();
                return user;
            }
            user = await AddUserContact(user, model);
            return user;
        }

        public async Task<ApplicationUser> AddUserContact(ApplicationUser user, ApplicationUser model)
        {
            UserContact contact = new UserContact()
            {
                Instagram = model.UserContact.Instagram,
                Facebook = model.UserContact.Facebook,
                Linkedin = model.UserContact.Linkedin,
                Gender = model.UserContact.Gender,
                BirthDate = model.UserContact.BirthDate,
                UserId = user.Id
            };
            await _context.UserContacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> EditUserWorkAddress(ApplicationUser user, ApplicationUser model)
        {
            var result = await _context.UserWorkAddresses.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (result != null)
            {
                UserWorkAddress workAddress = new UserWorkAddress()
                {
                    Country = !String.IsNullOrEmpty(model.WorkAddress.Country) ? model.WorkAddress.Country : user.WorkAddress.Country,
                    City = !String.IsNullOrEmpty(model.WorkAddress.City) ? model.WorkAddress.City : user.WorkAddress.City,
                    Street = !String.IsNullOrEmpty(model.WorkAddress.Street) ? model.WorkAddress.Street : user.WorkAddress.Street,
                    CompanyAddress = !String.IsNullOrEmpty(model.WorkAddress.CompanyAddress) ? model.WorkAddress.CompanyAddress : user.WorkAddress.CompanyAddress,
                    CompanyName = !String.IsNullOrEmpty(model.WorkAddress.CompanyName) ? model.WorkAddress.CompanyName : user.WorkAddress.CompanyName,
                    PostalCode = !String.IsNullOrEmpty(model.WorkAddress.PostalCode.ToString()) ? model.WorkAddress.PostalCode : user.WorkAddress.PostalCode,
                    UserId = String.IsNullOrEmpty(result.UserId.ToString()) ? model.Id : result.UserId 
                };
                _context.UserWorkAddresses.Update(workAddress);
                await _context.SaveChangesAsync();
                return user;
            }
            user = await AddUserWorkAddress(user, model);
            return user;
        }

        public async Task<ApplicationUser> AddUserWorkAddress(ApplicationUser user, ApplicationUser model)
        {
            UserWorkAddress userWorkAddress = new UserWorkAddress()
            {
                Country = model.WorkAddress.Country,
                City = model.WorkAddress.City,
                Street = model.WorkAddress.Street,
                CompanyAddress = model.WorkAddress.CompanyAddress,
                CompanyName = model.WorkAddress.CompanyName,
                UserId = user.Id
            };
            await _context.UserWorkAddresses.AddAsync(userWorkAddress);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> UserOrders(Guid id)
        {
            ApplicationUser userOrders = await _context.Users
                .Include(o => o.Order)
                /*.ThenInclude(c => c.CartItems)*/
                .FirstOrDefaultAsync(u => u.Id == id);
            return userOrders;
        }
    }
}
