using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Web.ViewModels.Admin.UserManagement;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UsersViewModel>> GetAllUsersAsync();
    }
}
