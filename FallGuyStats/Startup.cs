// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FallGuyStats.Data;
using Microsoft.Extensions.FileProviders;
using FallGuyStats.Services;
using FallGuyStats.Repositories;

namespace FallGuyStats
{
    public class Startup
    {
        readonly string AllowLocalOrigins = "_allowLocalOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddDbContext<FallGuysContext>(opt =>
                opt.UseSqlite(Configuration.GetConnectionString("FallGuysDb")));
            services.AddControllers();
            services.AddScoped<StatService>();
            services.AddScoped<EpisodeRepository>();
            services.AddCors(o =>
            {
                o.AddPolicy(AllowLocalOrigins, builder => {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FallGuysContext fallGuysContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            fallGuysContext.Database.EnsureCreated();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(AllowLocalOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
