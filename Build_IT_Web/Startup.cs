using Build_IT_WebApplication;
using Build_IT_WebInfrastructure;

namespace Build_IT_Web
{
    internal class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddWebUIServices();
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy(name: "CorsPolicy", builder =>
            //    {
            //        builder.WithOrigins(Configuration.GetValue<string>("AllowedClient"))
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseCors();

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}