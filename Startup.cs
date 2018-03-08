using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThreeDo.DbContext;

namespace ThreeDo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                // ASP.net: Environment variables are used when present, 
                // otherwise appsettings.json variables will be used. Both places must have the same variables and must implement AddEnvironmentVariables().
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                    .AllowCredentials());
            });
            // do some other work
            services.AddOptions();

            // Register the IConfiguration instance which AppOptions binds against.
            services.Configure<AppOptions>(Configuration);

            ConnectionSetting.DefaultConnection = Configuration.GetConnectionString("DefaultConnection");

            // Add the MVC feature
            //services.AddMvcCore()
                    //.AddJsonFormatters();

            services.AddMvc();

            //  Identity Server 
            //services.AddIdentityServer()
            //.AddTemporarySigningCredential()
            //.AddInMemoryApiResources(APIResourceProvider.GetApiResources())
            //.AddInMemoryClients(APIClientStore.AllClients);
            //      .AddAspNetIdentity<ApplicationUser>();
            //     .AddSigningCredential(new X509Certificate2());

            // Use PostgreSQL. ApplicationDbContext.cs sets the db connection, so you may not need the "options" object.
            //services.AddEntityFrameworkNpgsql()
            //        .AddDbContext<ApplicationDbContext>();//options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseIdentity();
            //app.UseIdentityServer();

            //app.UseCors(builder =>
            //          builder.WithOrigins("http://localhost:8080")
            //                 .AllowAnyHeader()
            //          );

            app.UseCors("CorsPolicy");

            app.UseMvc();
   
        }
    }
}
