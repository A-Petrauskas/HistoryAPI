using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Repositories;
using Services;
using Services.Interfaces;

namespace HistoryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<HistoryApiDatabaseSettings>(Configuration.GetSection(nameof(HistoryApiDatabaseSettings)));
            services.AddScoped<IHistoryApiDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<HistoryApiDatabaseSettings>>().Value);

            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IEventsService, EventsService>();

            services.AddScoped<ILevelsRepository, LevelsRepository>();
            services.AddScoped<ILevelsService, LevelsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
