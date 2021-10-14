using System.ComponentModel;
using System.Net.Http.Headers;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.Entity.Query.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Repositories.Implementations;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Implementations;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.WebApi;
using TechnicalRadiation.WebApi.Controllers;
using TechnicalRadiation.WebApi.Mapping;
using TechnicalRadiation.WebApi.ExceptionHandlerExtensions;

namespace TechnicalRadiation.WebApi
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
            var mappingProfile = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingProfile.CreateMapper();
            
            services.AddSingleton(mapper);
            services.AddControllers();
            services.AddMvc();
            services.AddScoped<INewsItemRepository, NewsItemRepository>();
            services.AddTransient<INewsItemService, NewsItemService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddDbContext<NewsDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("default"),
                    x => x.MigrationsAssembly("TechnicalRadiation.WebApi")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechnicalRadiation.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnicalRadiation.WebApi v1"));
            }
            
            app.UseHttpsRedirection();
            
            app.UseGlobalExceptionHandler();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
