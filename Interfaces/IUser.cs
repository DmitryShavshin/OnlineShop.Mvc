using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IUser
    {
        public ApplicationUser GetUserByName(string name);
        public Task<ApplicationUser> GetUserById (Guid id);
        public Task<ApplicationUser> GetAllUserData(Guid id);
        public ApplicationUser AddUserAvatar(Guid id, IFormFile file);
        public Task<ApplicationUser> AddUserHomeAddress(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> AddUserContact(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> AddUserWorkAddress(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> EditPhoneNumber(Guid id, string phone);
        public Task<ApplicationUser> EditEmail(Guid id, string email);
        public Task<ApplicationUser> EditUserInformation();
        public Task<ApplicationUser> EditHomeAddress(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> EditUserContact(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> EditUserWorkAddress(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> EditUser(Guid id, ApplicationUser model);
        public Task<ApplicationUser> CheckingPhoneNumber(ApplicationUser user, string phoneNumber);
        public Task<ApplicationUser> CheckingEmail(ApplicationUser user, string email);
        public Task<ApplicationUser> CheckingUserInformation(ApplicationUser user, ApplicationUser model);
        public Task<ApplicationUser> UserOrders(Guid id);
    }
}
