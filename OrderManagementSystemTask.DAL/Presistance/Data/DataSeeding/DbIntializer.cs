using Microsoft.AspNetCore.Identity;
using OrderManagementSystemTask.DAL.Entities;

namespace OrderManagementSystemTask.DAL.Presistance.Data.DataSeeding
{
    public class DbIntializer : IDbIntializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbIntializer(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task IntializIdentityAsync()
        {
            //Add roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            //Add User and assign role 
            if (!_userManager.Users.Any())
            {
                var userAdmin = new User
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01278637644"
                };
                await _userManager.CreateAsync(userAdmin, "P@ssw0rd");
                await _userManager.AddToRoleAsync(userAdmin, "Admin");
            }
        }
    }
}
