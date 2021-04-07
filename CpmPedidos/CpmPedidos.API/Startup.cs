using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data.Common;
using CpmPedidos.Repository;
using Microsoft.EntityFrameworkCore;

namespace CpmPedidos.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public DbConnection DbConnection => new NpgsqlConnection(Configuration.GetConnectionString("App"));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => 
            {
                options.UseNpgsql(
                    DbConnection,
                    assembly => assembly.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    );
            });

            DependencyInjection.Register(services);

            services.AddControllers()
                .AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
