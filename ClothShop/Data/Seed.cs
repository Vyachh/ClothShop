using ClothShop.Models;
using ClothShop.Models.Catalog;
using Microsoft.AspNetCore.Identity;

namespace ClothShop.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            //using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            //{
            //    //Items
            //    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            //    var item = new Item()
            //    {
            //        Id = "0",
            //        UserId = "1da45101-2152-423c-b82d-e08ad7d77a94",
            //        Name = "Кейс за писот",
            //        Image = "https://res.cloudinary.com/ddfsfpulm/image/upload/v1682091350/hk5k4uu9qc099gocwhvy.jpg",
            //        Price = 500,
            //        Description = "Case",
            //        Quantity = 1,
            //        SizeFeatures = new Size() {
            //            Width = 100,
            //            Height = 100,
            //            Depth = 100,
            //            Id = 0
            //        },
            //        Category = Enum.Category.Case
            //   };
            //    await userManager.AddAsync(item);
            //}

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "vyachhw@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Vyachh",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            City = "Киров",
                            Street = "Лепсе",
                            HouseNumber = 61,
                            Region = "Кировская область"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "SxeKgu12345!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }                  
            }
        }
    }
}
