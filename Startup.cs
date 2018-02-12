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

namespace ThreeDo
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
            //services.AddMvc();
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

            // Add the MVC feature
            services.AddMvcCore()
                    .AddJsonFormatters();

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
