using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Repository.DAL.DataInitializer
{
    public static class AppBuilderDataInitializer
    {
        public async static void Seed(this IApplicationBuilder builder)
        {
            using (IServiceScope scope = builder.ApplicationServices.CreateScope())
            {
                AppDbContext dbContext = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider
                  .GetRequiredService<RoleManager<IdentityRole>>();
                if (!dbContext.Roles.Any())
                {
                   await roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
                   await roleManager.CreateAsync(new IdentityRole { Name = "User" });
                   await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
