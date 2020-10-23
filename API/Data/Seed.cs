using System.Threading.Tasks;
using Microsoft.AspNetCore;
using System.Text.Json;
using API.Entities;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName  = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);

            }
            await context.SaveChangesAsync();
        }
    }
}