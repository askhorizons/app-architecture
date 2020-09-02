using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, UserManager<User> userManager)
        {
            var path = "../Infrastructure/Data/";
            try
            {
                if (!userManager.Users.Any())
                {
                    var usersData = System.IO.File.ReadAllText(path + "users-data.json");

                    var users = JsonConvert.DeserializeObject<List<User>>(usersData);
                    var logger = loggerFactory.CreateLogger<DataSeeder>();

                    foreach (var item in users)
                    {
                        var user = new User
                        {
                            FirstName = item.FirstName,
                            Created = DateTime.Now,
                            Email = item.Email,
                            LastName = item.LastName,
                            PhoneNumber = item.PhoneNumber,
                            UserName = item.UserName,
                        };

                        var result = await userManager.CreateAsync(user, "password");
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"User {user} created successfully");
                        }
                        else
                        {
                            logger.LogError($"Error creating user");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataSeeder>();
                logger.LogError(ex.Message);
            }
        }
    }
}
