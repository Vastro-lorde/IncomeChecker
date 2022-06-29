using Microsoft.OpenApi.Models;
using System.Reflection;

namespace IncomeChecker.Services
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            //Some swagger UI settings
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Income Checker",
                    Description = "An ASP.NET Core Web API for checking a customers income using mono api",
                    Contact = new OpenApiContact
                    {
                        Name = "Omatsolaseund@gmail.com",
                        Url = new Uri("mailto:omatsolaseund@gmail.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Seun Daniel Omatsola CC License",
                        Url = new Uri("https://github.com/Vastro-lorde")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the input below. \r\n\r\n Example : 'Bearer 124fsfs'"
                }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

        }
    }
}
