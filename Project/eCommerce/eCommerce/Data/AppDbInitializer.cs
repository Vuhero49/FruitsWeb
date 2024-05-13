using eCommerce.Data.Static;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				context.Database.EnsureCreated();

                //Store
      //          if(!context.Stores.Any()) 
      //          {
      //              context.Stores.AddRange(new List<Store>()
      //              {
      //                  new Store() 
      //                  {
      //                      Name = "Winmart",
      //                      Logo = "https://timxungquanh.com/UserFile/timxungquanh/CompanyImage/e1717179-0cac-4168-9fc1-f1d895c042be.png",
      //                      Description = "The small supermarket",
      //                  },

      //                  new Store() 
      //                  {
      //                      Name = "Emart",
      //                      Logo = "https://emart24.vwebz.com/wp-content/uploads/emart24-logo-fb.jpg",
      //                      Description = "Goods Store",
      //                  },

      //                  new Store()
      //                  {
      //                      Name = "Grocert Store",
      //                      Logo = "https://cdn4.vectorstock.com/i/1000x1000/98/18/logo-for-grocery-store-vector-21609818.jpg",
      //                      Description = "We got everything you need",
      //                  },

      //                  new Store()
      //                  {
      //                      Name = "Foody Store",
      //                      Logo = "https://i.pinimg.com/originals/54/6f/20/546f20b7e2fff01bd97de9fada1d9ab8.png",
      //                      Description = "Just a store",
      //                  },

      //                  new Store()
      //                  {
      //                      Name = "Shopping Vegetables",
      //                      Logo = "https://img.freepik.com/premium-vector/shopping-vegetables-logo_48832-1247.jpg",
      //                      Description = "Shopping Time",
      //                  }

      //              });           
      //              context.SaveChanges();
      //          }

      //          //Company
      //          if (!context.Companies.Any())
      //          {
      //              context.Companies.AddRange(new List<Company>()
      //              {
      //                  new Company() 
      //                  {
      //                      FullName = "Company 1",
      //                      Bio = "This is the bio of the first Company",
      //                      ProfilePicture = "https://static.vecteezy.com/system/resources/thumbnails/010/403/606/small/vegetable-logo-emblem-vintage-design-creative-idea-design-inspiration-free-vector.jpg"
						//},

      //                  new Company()
      //                  {
      //                      FullName = "Company 2",
      //                      Bio = "This is the bio of the second Company",
      //                      ProfilePicture = "https://static.vecteezy.com/system/resources/thumbnails/005/432/299/small/vegetable-illustration-for-logo-vector.jpg"
						//},

      //                  new Company()
      //                  {
      //                      FullName = "Company 3",
      //                      Bio = "This is the bio of the third Company",
      //                      ProfilePicture = "https://www.shutterstock.com/image-vector/fruit-vegetables-logo-260nw-1360668347.jpg"
						//},
      //                  new Company()
      //                  {
      //                      FullName = "Company 4",
      //                      Bio = "This is the bio of the fourth Company",
      //                      ProfilePicture = "https://static.vecteezy.com/system/resources/thumbnails/009/732/780/small/fresh-vegetable-logo-healthy-food-shop-illustration-vector.jpg"
						//},
      //                  new Company()
      //                  {
      //                      FullName = "Company 5",
      //                      Bio = "This is the bio of the fifth Company",
      //                      ProfilePicture = "https://img.freepik.com/free-vector/hand-drawn-farmers-market-logo_23-2149329270.jpg"
						//},
      //              });
      //              context.SaveChanges();
      //          }

      //          //City
      //          if (!context.Cities.Any())
      //          {
      //              context.Cities.AddRange(new List<City>() 
      //              {
      //                  new City() 
      //                  {
      //                      FullName = "Banana City",
      //                      Bio = "Banana is love, Banana is life",
      //                      ProfilePicture ="https://img.freepik.com/premium-vector/city-banana-logo_48832-1490.jpg?size=626&ext=jpg"
						//},

      //                  new City()
      //                  {
      //                      FullName = "Tea City",
      //                      Bio = "My tea is the best of the world",
      //                      ProfilePicture ="https://img.freepik.com/premium-vector/city-tea-logo_48832-1516.jpg?size=626&ext=jpg"
						//},

      //                  new City()
      //                  {
      //                      FullName = "Vegetable City",
      //                      Bio = "Fresh 100%",
      //                      ProfilePicture ="https://img.freepik.com/premium-vector/city-vegetables-logo_48832-1517.jpg?w=2000"
						//},

      //                  new City()
      //                  {
      //                      FullName = "Fruits City",
      //                      Bio = "Fruits is my life",
      //                      ProfilePicture ="https://img.freepik.com/premium-vector/city-fruits-logo_48832-1495.jpg?size=626&ext=jpg"
						//},

      //                  new City()
      //                  {
      //                      FullName = "Seasons City",
      //                      Bio = "Everything makes perfect",
      //                      ProfilePicture ="https://img.freepik.com/premium-vector/city-seasons-logo_48832-1511.jpg?size=626&ext=jpg"
						//},
      //              });
      //              context.SaveChanges();
      //          }

      //          //Product
      //          if (!context.Products.Any())
      //          {
      //              context.AddRange(new List<Product>() 
      //              {
      //                  new Product()
      //                  {
      //                      Name = "Red Apple",
      //                      Description = "Delicious, 100% Fresh, Try it now",
      //                      Price = 3.5,
      //                      ImageURL = "https://i.pinimg.com/originals/a9/d8/69/a9d869b2b5271106890e1a3f7f320952.png",
      //                      StartDay = DateTime.Now,
      //                      EndDay = DateTime.Now.AddDays(10),
      //                      CityId = 4,
      //                      StoreId = 1,
      //                      ProductCategory = Enum.ProductCategory.Fruit
      //                  },

      //                  new Product()
      //                  {
      //                      Name = "Cabbage",
      //                      Description = "Green, Fresh, No Poison, Good for Kids",
      //                      Price = 5.0,
      //                      ImageURL = "https://health.clevelandclinic.org/wp-content/uploads/sites/3/2022/09/Benefits-Of-Cabbage-589153824-770x533-1-350x350.jpg",
      //                      StartDay = DateTime.Now,
      //                      EndDay = DateTime.Now.AddDays(5),
      //                      CityId = 3,
      //                      StoreId = 2,
      //                      ProductCategory = Enum.ProductCategory.Vegetable
      //                  },

      //                  new Product()
      //                  {
      //                      Name = "Brown Rice",
      //                      Description = "Healthy, Good for your heath",
      //                      Price = 7.6,
      //                      ImageURL = "https://5.imimg.com/data5/SELLER/Default/2021/9/YQ/CY/XK/96607656/long-brown-rice-500x500.jpg",
      //                      StartDay = DateTime.Now,
      //                      EndDay = DateTime.Now.AddDays(60),
      //                      CityId = 5,
      //                      StoreId = 3,
      //                      ProductCategory = Enum.ProductCategory.Rice
      //                  },

      //                  new Product()
      //                  {
      //                      Name = "Matcha",
      //                      Description = "The most popular tea in the world",
      //                      Price = 9.8,
      //                      ImageURL = "https://www.reviewbox.com.mx/wp-content/uploads/2020/01/te-de-matcha-4-600x450.jpg",
      //                      StartDay = DateTime.Now,
      //                      EndDay = DateTime.Now.AddDays(60),
      //                      CityId = 2,
      //                      StoreId = 4,
      //                      ProductCategory = Enum.ProductCategory.Tea
      //                  },

      //                  new Product()
      //                  {
      //                      Name = "Banana",
      //                      Description = "Boost your energy, Good for your health",
      //                      Price = 4.5,
      //                      ImageURL = "https://www.pureearete.com/wp-content/uploads/2020/06/banana_m.jpg",
      //                      StartDay = DateTime.Now,
      //                      EndDay = DateTime.Now.AddDays(7),
      //                      CityId = 1,
      //                      StoreId = 5,
      //                      ProductCategory = Enum.ProductCategory.Fruit
      //                  },
      //              });
      //              context.SaveChanges();
      //          }

      //          //Company_Product
      //          if (!context.Company_Products.Any())
      //          {
      //              context.AddRange(new List<Company_Product>() 
      //              {
      //                  new Company_Product() 
      //                  {
      //                      CompanyId = 4,
      //                      ProductId = 1,
      //                  },
      //                  new Company_Product() 
      //                  {
      //                      CompanyId= 3,
      //                      ProductId= 1,
      //                  },
      //                  new Company_Product() 
      //                  {
      //                      CompanyId = 3,
      //                      ProductId= 2,
      //                  },
      //                  new Company_Product() 
      //                  {
      //                      CompanyId = 1,
      //                      ProductId= 5,
      //                  },
      //                  new Company_Product() 
      //                  {
      //                      CompanyId = 5,
      //                      ProductId= 3,
      //                  },
      //                  new Company_Product()
      //                  {
      //                      CompanyId = 2,
      //                      ProductId= 4,
      //                  },
      //                  new Company_Product()
      //                  {
      //                      CompanyId = 5,
      //                      ProductId= 4,
      //                  },
      //                  new Company_Product()
      //                  {
      //                      CompanyId = 4,
      //                      ProductId= 4,
      //                  },
      //              });
      //              context.SaveChanges();
      //          }

                // Roles
				if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
				{
					var adminRole = new IdentityRole(UserRoles.Admin);
					roleManager.CreateAsync(adminRole).Wait();
				}

				if (!roleManager.RoleExistsAsync(UserRoles.User).Result)
				{
					var userRole = new IdentityRole(UserRoles.User);
					roleManager.CreateAsync(userRole).Wait();
				}

                //// Thêm tài khoản admin
                //var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //var adminUser = userManager.FindByEmailAsync("admin@ecommerce.com").Result;

                //if (adminUser == null)
                //{
                //    var newAdminUser = new ApplicationUser()
                //    {
                //        FullName = "Admin User",
                //        UserName = "admin-user",
                //        Email = "admin@ecommerce.com",
                //        EmailConfirmed = true
                //    };
                //    var result = userManager.CreateAsync(newAdminUser, "Coding@1234?").Result;

                //    if (result.Succeeded)
                //    {
                //        userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin).Wait();
                //    }
                //}

                //Thêm tài khoản user
                //var appUser = userManager.FindByEmailAsync("user@ecommerce.com").Result;

                //if (appUser == null)
                //{
                //    var newAppUser = new ApplicationUser()
                //    {
                //        FullName = "Application User",
                //        UserName = "app-user",
                //        Email = "user@ecommerce.com",
                //        EmailConfirmed = true
                //    };
                //    var result = userManager.CreateAsync(newAppUser, "Coding@1234?").Result;

                //    if (result.Succeeded)
                //    {
                //        userManager.AddToRoleAsync(newAppUser, UserRoles.User).Wait();
                //    }
                //}
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@ecommerce.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@ecommerce.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
