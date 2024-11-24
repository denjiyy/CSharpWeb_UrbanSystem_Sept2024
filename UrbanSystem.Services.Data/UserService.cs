using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Admin.UserManagement;

namespace UrbanSystem.Services.Data
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UsersViewModel>> GetAllUsersAsync()
        {
            ApplicationUser[] allUsers = _userManager.Users
                .ToArray();
            ICollection<UsersViewModel> allUsersViewModel = new List<UsersViewModel>();

            foreach (ApplicationUser user in allUsers)
            {
                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

                allUsersViewModel.Add(new UsersViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Roles = roles
                });
            }

            return allUsersViewModel;
        }
    }
}
