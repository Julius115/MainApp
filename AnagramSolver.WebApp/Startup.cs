using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.EF.DatabaseFirst.Repositories;
using AnagramSolver.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Repositories;

namespace AnagramSolver.WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=AnagramsCFDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<AnagramsDbCfContext>(options => options.UseSqlServer(connection));

            //services.AddScoped<AnagramsDBContext, AnagramsDBContext>();
            
            services.AddScoped<IWordRepository, EFCFDictionaryWordRepository>();
            services.AddScoped<IAnagramSolver, AnagramSolverSingleWord>();
            services.AddScoped<IDatabaseManager, EFCFControlRepository>();
            services.AddScoped<ICachedWords, EFCFCachedWordRepository>();
            services.AddScoped<ILogger, EFCFUserLogRepository>();
            services.AddScoped<AnagramsSearchService, AnagramsSearchService>();
            services.AddScoped<IRequestWordContract, EFCFRequestWordRepository>();
            services.AddScoped<IUserContract, EFCFUserRepository>();
            services.AddScoped<DictionaryManagingService, DictionaryManagingService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
