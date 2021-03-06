﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionName.ApiTemplate.Error;
using SolutionName.Core.Interfaces.Repositories;
using SolutionName.Core.Interfaces.Services;
using SolutionName.Core.Services;
using SolutionName.Infrastructure.Data;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace SolutionName.ApiTemplate
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
            //Register database context
            services.AddDbContext<ExampleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ExampleConnectionString")));

            services.AddMvc(o =>
            {
                o.Filters.Add<ValidateModelStateFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Register your services
            services.AddTransient<IExampleService, ExampleService>();

            //Register repositories
            services.AddScoped<IExampleRepository, ExampleRepository>();

            services.AddSingleton(Configuration);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Example API v1", Version = "v1" });
                //c.OperationFilter<AddTokenHeaderParameter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            //Swagger setup
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API v1");
                c.RoutePrefix = string.Empty;
            });

            //Add middleware to handle errors
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMvc();
        }
    }
}
