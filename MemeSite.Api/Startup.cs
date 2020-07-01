using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using MemeSite.Data.DbContext;
using MemeSite.Api.Middleware;
using AutoMapper;
using MemeSite.Api.AutoMapper;
using MemeSite.Api.Configuration;
using MemeSite.Domain.Common;
using MemeSite.Domain;

namespace MemeSite.Api
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
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                      .SelectMany(v => v.Errors)
                      .Select(v => v.ErrorMessage));

                     var result = new Result();
                     result.Errors.Add(errors);


                    return new BadRequestObjectResult(result);
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            //db+identity
            services.AddDbSetup(Configuration);

            //JwtAuth
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.JWTsetup(key);

            //repositories, services, validators
            services.AddRepositoryServicesSetup();

            // Auto Mapper Configurations
            services.AddAutoMapperSetup();

            //swagger
            services.AddSwaggerSetup();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, UserManager<PageUser> userManager, RoleManager<PageRole> roleManager)
        {
            app.UseCors("AllowAll");

            app.UseMiddleware<ExceptionsHandlingMiddleware>();

            DataSeeder.SeedData(userManager, roleManager);

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            //swagger
            app.UseSwaggerSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
