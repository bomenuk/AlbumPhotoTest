using AlbumPhotoFetchService;
using AlbumPhotoFetchService.Contracts;
using AlbumPhotoFetchService.Contracts.Entities;
using Caching.Contracts;
using Caching.MemoryBased;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contracts;
using Repository.UrlBased;

namespace AlbumPhotoTest
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IAlbumPhotoService, AlbumPhotoService>();
            services.AddSingleton<ICacheClient<baseServiceEntity>, MemoryBasedCacheClient<baseServiceEntity>>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
