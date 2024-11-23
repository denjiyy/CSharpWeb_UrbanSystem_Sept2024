using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Configuration
{
    public class RolesSeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RolesSeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }


    }
}
