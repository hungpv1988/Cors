using Cors.CorsComponent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Cors
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cors", Version = "v1" });
            });

            //  if needed, add policy here instead of UseCors in Configure 
            //be careful, if add slash(https://localhost:44371/) this will not work

            var corsPolicy = new CorsPolicy();
            corsPolicy.Origins.Add("https://localhost:44371");
            services.AddCors(option => option.AddPolicy(Constants.DefaultCorsPolicy, corsPolicy));
            services.AddTransient<ICorsPolicyProvider, OurCorsPolicyProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cors v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // add user Cors
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // this is to use middle ware, not OurCorsPolicyProvider, so adding policy here is fine.
            // remember to comment those lines of code if you use OurCorsPolicyProvider
            //var corsPolicy = new CorsPolicy();
            //corsPolicy.Origins.Add("https://localhost:44371");
            //app.UseMiddleware<CorsMiddleWare>(Constants.DefaultCorsPolicy, corsPolicy);
        }
    }
}
