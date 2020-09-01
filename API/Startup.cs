using System;
using API.Extensions;
using API.Utils;
using AutoMapper;
using AutoWrapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        private readonly IConfiguration _Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(x =>
               x.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<AppDbContext>();

            services.AddControllers();

            //Add Auto Mapper
            services.AddAutoMapper(typeof(AutoMapping));

            //Add Extension for dependency injections
            services.AddApplicationServices();


            //Add Cors
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:4200");
            }));

            //Add Identity Services
            services.AddIdentityServices(_Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Service",
                    Version = "v1",
                    Description = "Build with C# ASP.NET CORE + Angular Typescript",
                    Contact = new OpenApiContact
                    {
                        Name = "Salman Malik",
                        Email = "salman1277@gmail.com"
                    },
                    TermsOfService = new Uri("https://github.com/askhorizons")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMiddleware<ExceptionMiddleware>();
            // app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "App Solutions");
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            // app.UseHttpsRedirection();

            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IgnoreNullValue = false,
                ShowStatusCode = true,
                UseCamelCaseNamingStrategy = true,
                ApiVersion = "version 1.0",
                ShowApiVersion = true,
                UseCustomSchema = true
            });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
