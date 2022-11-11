using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Models.Support.Serialization;
using OPM0PG_HFT_2022231.Repository;

namespace OPM0PG_HFT_2022231.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()
                    .Error;

                var response = new { Error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MusicDbContext>();
            services.AddTransient<IMusicRepository, MusicRepository>();
            services.AddTransient<IAlbumLogic, AlbumLogic>();
            services.AddTransient<IArtistLogic, ArtistLogic>();
            services.AddTransient<IContributionLogic, ContributionLogic>();
            services.AddTransient<IGenreLogic, GenreLogic>();
            services.AddTransient<IReleaseLogic, ReleaseLogic>();
            services.AddTransient<IMediaLogic, MediaLogic>();
            services.AddTransient<IMusicLogic, MusicLogic>();

            services.AddMvc().AddNewtonsoftJson(setup =>
            {
                foreach (var item in ModelJsonSerializer.GetConverters())
                {
                    setup.SerializerSettings.Converters.Add(item);
                }
            });
        }
    }
}