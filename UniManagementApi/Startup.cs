using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementApi.AuthO;
using UniManagementApi.Services;
using UniPortalModel;

namespace UniManagementApi
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
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", builder =>
            //    {
            //        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            //    });

            //});


            services.AddEntityFrameworkSqlServer()
                .AddDbContext<UniPortalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ITeacherService, TeacherService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            if (env.IsDevelopment())
            {

                var allowedOrigins = Configuration.GetSection("AppSettings:CORS-Settings:Allow-Origins").Get<string[]>();
                var allowedMethods = Configuration.GetSection("AppSettings:CORS-Settings:Allow-Methods").Get<string[]>();
                if (allowedOrigins?.Length > 0)
                {

                    app.UseCors(x => x
                    .WithOrigins(allowedOrigins)
                    .WithMethods(allowedMethods).AllowAnyHeader());
                }
            }
            else
            {
                app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            }



            // app.UseCors(builder => builder
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Admin}/{action=Start}");
            });
        }
    }
}
