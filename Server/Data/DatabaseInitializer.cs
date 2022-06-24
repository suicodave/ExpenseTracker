using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Server.Users;

namespace Server.Data
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(ApplicationDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task Run()
        {
            _dbContext.Database.Migrate();

            await _roleManager.CreateAsync(new Role
            {
                Name = Role.OWNER
            });

            await _roleManager.CreateAsync(new Role
            {
                Name = Role.ADMINISTRATOR
            });


            await _roleManager.CreateAsync(new Role
            {
                Name = Role.CLERK
            });

            await _roleManager.CreateAsync(new Role
            {
                Name = Role.ACCOUNTANT
            });

            string email = _configuration.GetValue<string>("DefaultAdministrator:Email");

            string password = _configuration.GetValue<string>("DefaultAdministrator:Password");

            if (email is not null)
            {
                await _userManager.CreateAsync(new User
                {
                    Email = email,
                    UserName = email
                }, password);
            }


        }



    }
}