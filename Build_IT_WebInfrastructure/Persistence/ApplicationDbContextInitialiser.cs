﻿using Build_IT_WebInfrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_WebInfrastructure.Persistence
{

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole("Administrator");
            var designerRole = new IdentityRole("Designer");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
                await _roleManager.CreateAsync(administratorRole);
            if (_roleManager.Roles.All(r => r.Name != designerRole.Name))
                await _roleManager.CreateAsync(designerRole);

            // Default users
            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
            var designer1 = new ApplicationUser { UserName = "designer@localhost", Email = "designer@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
            if (_userManager.Users.All(u => u.UserName != designer1.UserName))
            {
                await _userManager.CreateAsync(designer1, "Designer1!");
                await _userManager.AddToRolesAsync(designer1, new[] { designerRole.Name });
            }

            await _context.SaveChangesAsync();
        }
    }
}
