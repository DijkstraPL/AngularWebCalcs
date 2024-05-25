using Build_IT_Web.Filters;
using Build_IT_Web.Services;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebInfrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Build_IT_Web
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false)
                    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddRazorPages();

            services.SetupApiBehaviours();

            services.AddOpenApiDocument();

            return services;
        }

        public static IServiceCollection SetupApiBehaviours(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
        public static IServiceCollection AddOpenApiDocument(this IServiceCollection services)
        {
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Build_IT_Web API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //    {
            //        Title = "Build_IT_Web",
            //        Version = "v1",
            //        Description = "Set of civil engineering calculators"
            //    });

            //    c.AddSecurityDefinition("JWT", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.ApiKey,
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Description = "Type into the textbox: Bearer {your JWT token}."
            //    });

            //    c.OperationFilter<SecurityRequirementsOperationFilter>("JWT"); 

            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});
            return services;
        }
    }
}