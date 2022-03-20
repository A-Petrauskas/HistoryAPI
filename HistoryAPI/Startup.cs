using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
            services.AddCors();
            services.AddControllers();

            // Adding Swagger documentation
            services.AddSwaggerGen(options =>
            {
                var swaggerSection = Configuration.GetSection("Swagger");
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = swaggerSection.GetValue<string>("Title"),
                    Description = swaggerSection.GetValue<string>("Description"),
                    Version = swaggerSection.GetValue<string>("Version")
                });
            });


            // Adding AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();


            // Dependency injections
            services.Configure<HistoryApiDatabaseSettings>(Configuration.GetSection(nameof(HistoryApiDatabaseSettings)));
            services.AddScoped<IHistoryApiDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<HistoryApiDatabaseSettings>>().Value);

            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IEventsService, EventsService>();

            services.AddScoped<ILevelsRepository, LevelsRepository>();
            services.AddScoped<ILevelsService, LevelsService>();
            services.AddSingleton(mapper);

            services.AddScoped<IGameService, GameService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger(options =>
            {
                options.RouteTemplate = "history/swagger/{documentname}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "history/swagger";
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "History Timeline API");
            });
        }
    }
}
