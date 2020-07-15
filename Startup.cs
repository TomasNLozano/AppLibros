using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibros.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Session;

namespace AppLibros
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
            services.AddDbContext<LibrosDataBaseContext>(
               options =>
                options.UseSqlServer(Configuration["ConnectionString:ResenasDataBaseConnection"
            ]));
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "{controller=Home}/{action=LogIn}/{username?}/{password?}"
                    );
                endpoints.MapControllerRoute(
                    name: "agregarLibroFav",
                    pattern: "{controller=Libro}/{action=agregarFavorito}/{id}"
                    );
                endpoints.MapControllerRoute(
                    name: "agregarAutorFav",
                    pattern: "{controller=Autor}/{action=agregarFavorito}/{id}"
                    );
                endpoints.MapControllerRoute(
                    name: "puntuar",
                    pattern: "{controller=Libro}/{action=puntuar}/{idLibro}/{puntaje}"
                    );
                endpoints.MapControllerRoute(
                   name: "Search",
                   pattern: "{controller=Libro}/{action=Search}/{testo}"
                   );
                endpoints.MapControllerRoute(
                   name: "Search",
                   pattern: "{controller=Autor}/{action=Search}/{testo}"
                   );
                endpoints.MapControllerRoute(
                   name: "Edit",
                   pattern: "{controller=Libro}/{action=Edit}/{id}/{titulo}/{isbn}/{resena}"
                   );
            });
        }
    }
}
