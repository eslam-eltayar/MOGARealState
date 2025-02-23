
using MOGARealState.APIs.Extensions;

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

            // Ensure roles are created at startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await InitializeRoles.InitializeRolesAsync(services);
            }

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
