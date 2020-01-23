using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServiceContext>(opt =>
                opt.UseInMemoryDatabase("ServiceLayer"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ServiceLayer API",
                    Description = "This Service Layer ASP.NET Core Web API",
                    TermsOfService = new Uri("https://about.me/vadimvlk"),
                    Contact = new OpenApiContact
                    {
                        Name = "Vadim Volkov",
                        Email = "vadimvlk0705@gmail.com",
                        Url = new Uri("http://srvdev.gq"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT"
                    }
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
