using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess.SnowLoads.Repositories.Interfaces;
using Build_IT_DataAccess.SteelProfiles.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.DeadLoads;
using Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces;
using Build_IT_DataAccess_SqlServer.DeadLoads.Repositories;
using Build_IT_DataAccess_SqlServer.Projects;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Repositories;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories;
using Build_IT_DataAccess_SqlServer.SnowLoads.Interfaces;
using Build_IT_DataAccess_SqlServer.SnowLoads.Repositories;
using Build_IT_DataAccess_SqlServer.SteelProfiles;
using Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces;
using Build_IT_DataAccess_SqlServer.SteelProfiles.Repositories;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Identity;
using Build_IT_WebApplication.Interfaces;
using Build_IT_WebInfrastructure.Identity;
using Build_IT_WebInfrastructure.Persistence;
using Build_IT_WebInfrastructure.Persistence.Interceptors;
using Build_IT_WebInfrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Build_IT_WebInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddSingleton<IAppConfigurationProvider, AppConfigurationProvider>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("BuildItDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>((services, options) =>
                    options.UseSqlServer(
                        services.GetService<IAppConfigurationProvider>().GetConnectionString(),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDataCache>(provider =>
            {
                var redisUrl = provider.GetService<IAppConfigurationProvider>().GetRedisUrl();
                if (string.IsNullOrEmpty(redisUrl))
                    return new MemoryCache(provider.GetService<IDateTime>());
                return new RedisCache(ConnectionMultiplexer.Connect(redisUrl), provider.GetService<IDateTime>());
            });

            services.AddScoped<ISteelProfilesDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IScriptInterpreterDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IProjectsDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDeadLoadsDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<ISnowLoadsDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddRepositories(configuration);

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IUserService, UserService>();

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
            {
                options.IssuerUri = configuration["AllowedClient"];
            })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
               .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            AddDeadLoads(services, configuration);
            AddSnowLoads(services, configuration);
            AddProjects(services, configuration);
            AddScriptInterpreter(services, configuration);
            AddSteelProfiles(services, configuration);
            return services;
        }

        private static void AddSteelProfiles(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISteelProfilesUnitOfWork, SteelProfilesUnitOfWork>();

            services.AddScoped<IProfileTypeRepository, ProfileTypeRepository>();
        }

        private static void AddScriptInterpreter(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScriptInterpreterUnitOfWork, ScriptInterpreterUnitOfWork>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IScriptRepository, ScriptRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITestDataRepository, TestDataRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();

        }

        private static void AddProjects(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProjectsUnitOfWork, ProjectsUnitOfWork>();

            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IDeadLoadLayerRepository, DeadLoadLayerRepository>();
            services.AddScoped<IDesignerRepository, DesignerRepository>();
            services.AddScoped<IParameterInputRepository, ParameterInputRepository>();
            services.AddScoped<IProjectDeadLoadRepository, ProjectDeadLoadRepository>();
            services.AddScoped<IDeadLoadRepository, DeadLoadRepository>();
            services.AddScoped<IProjectDesignerClaimRepository, ProjectDesignerClaimRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectScriptRepository, ProjectScriptRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserCompanyRepository,  UserCompanyRepository>();
        }

        private static void AddDeadLoads(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDeadLoadsUnitOfWork, DeadLoadsUnitOfWork>();

            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISubcategoryRepository, SubcategoryRepository>();
        }
        private static void AddSnowLoads(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISnowLoadsUnitOfWork, SnowLoadsUnitOfWork>();

            services.AddTransient<ISnowLoadRepository, SnowLoadRepository>();
            services.AddTransient<IProjectSnowLoadRepository, ProjectSnowLoadRepository>();
        }
    }
}
