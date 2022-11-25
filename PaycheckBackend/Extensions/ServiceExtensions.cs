using PaycheckBackend.Db;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using PaycheckBackend.Repositories;
using PaycheckBackend.Repositories.Interfaces;

namespace PaycheckBackend.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
              builder => builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            });
        }

        public static void ConfigurePostgresqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["PgConnection:ConnectionString"];

            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}