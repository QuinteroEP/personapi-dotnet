using Microsoft.OpenApi.Models;

namespace personapi_dotnet.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Created the Swagger document
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Version .Net 9.0.x",
                    Title = ".NET PersonB Swagger UI",
                    Description = "Arquitectura de Software, Universidad Javeriana 2025 (Juan Diego Romero, Pablo Enrique Quintero y Kamilt Bejarano Diaz)"
                });

                // form 2 to generate the swagger documentation
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.XML", SearchOption.TopDirectoryOnly))
                {
                    c.IncludeXmlComments(filePath: name);
                }
            });

            return services;
        } // end method AddSwagger
    }
}
