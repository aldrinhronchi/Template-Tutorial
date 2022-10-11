using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Pholium.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Template .Net Core",
                    Version = "v1",
                    Description = "Tutorial de dev com .Net Core e angular do nic fontes",
                    Contact = new OpenApiContact
                    {
                        Name = "Aldrin Ronchi",
                        Email = "work.aldrinronchi@gmail.com"
                    }
                });

                String xmlPath = Path.Combine("wwwroot", "api-doc.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }
        public static IApplicationBuilder UseSwaggerConfigutarion(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = "documentation";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1");
            });
        }
    }
}