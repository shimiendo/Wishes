using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProWishes.Repository;

namespace ProWishes.WebApi
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
            services.AddDbContext<ProWishesContext>(
                x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryProduct, RepositoryProduct>();
            services.AddScoped<IRepositoryWishe, RepositoryWishe>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);            

            //Configurando o CORS
            services.AddCors();            
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
            //CORS
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();

            app.UseMvc(routes =>
            {
            routes.MapRoute("page", "User/Pages/{page}",
                        defaults: new { controller = "User", action = "Index" });

            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
