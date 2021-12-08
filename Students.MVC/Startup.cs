using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Students.Application.Interface;
using Students.Application.Services;
using Students.Core.Repositories;
using Students.Core.Repositories.Base;
using Students.Infrastructure.Data;
using Students.Infrastructure.Repositories;
using Students.Infrastructure.Repositories.Base;

namespace Students.MVC
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
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddDbContext<StudentsContext>(options =>
             options.UseSqlite(
                 Configuration.GetConnectionString("DefaultConnection")
                 ));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Students}/{action=Index}/{id?}");
            });
        }
    }
}
