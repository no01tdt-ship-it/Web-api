using Microsoft.Extensions.DependencyInjection;
using Web_api.Interfaces.IRepositories;
using Web_api.Interfaces.IServices;
using Web_api.Repositories;
using Web_api.Services;

namespace Web_api.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // 1.Đăng ký nhóm Repositories
            services.AddScoped<IStudentRepository, StudentRepository>();

            // 2. Đăng ký nhóm Services
            services.AddScoped<IStudentService, StudentService>();



            
            return services;

        }

    }
}
