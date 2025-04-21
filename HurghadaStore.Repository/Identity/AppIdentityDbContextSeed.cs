using HurghadaStore.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Elsayed",
                    Email = "mahmoudelsayedofficial@outlook.com",
                    UserName = "mahmoud.elsayed",
                    PhoneNumber = "01234567899",
                };
                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
