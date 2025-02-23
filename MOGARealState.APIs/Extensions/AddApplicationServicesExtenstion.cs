using Microsoft.EntityFrameworkCore;
using MOGARealState.Core.Repositories;
using MOGARealState.Core.Services;
using MOGARealState.Repositories._Data;
using MOGARealState.Repositories.Repositories;
using MOGARealState.Services;

namespace MOGARealState.APIs.Extensions
{
    public static class AddApplicationServicesExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Services.AddDbContext<AppIdentityDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            //});

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IPropertyService, PropertyService>();
            Services.AddScoped<IFileUploadService, FileUploadService>();
            Services.AddScoped<IAgentService, AgentService>();


            return Services;
        }
    }
}
