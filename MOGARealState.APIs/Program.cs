
using Microsoft.AspNetCore.Identity;
using MOGARealState.APIs.Extensions;
using MOGARealState.Core.Entities;
using MOGARealState.Repositories._Data;
using MOGARealState.Repositories._Identity;

namespace MOGARealState.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();

            builder.Services.AddCors();

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityServices(builder.Configuration);


            var app = builder.Build();


            #region Apply All Pending Migrations [Update Database] and Data Seeding

            // Create a single scope for all database initialization operations

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                // Initialize roles first
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await InitializeRoles.InitializeRolesAsync(services);

                // Then create and assign users
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await ApplicationIdentityDbContextSeed.SeedUserAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error Has Been occurred during Seeding Data.");
            }

            #endregion

            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    await InitializeRoles.InitializeRolesAsync(services);
            //}

            //if (app.Environment.IsDevelopment())
            //{
            //app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(options =>
                        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                        );

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
