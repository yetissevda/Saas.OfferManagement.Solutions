using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Saas.Business.DependencyResolvers.Autofac;
using Saas.Core.DependencyResolvers;
using Saas.Core.Extensions;
using Saas.Core.Security.Security.Encyption;
using Saas.Core.Utilities.IoC;
using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using TokenOptions = Saas.Core.Security.Security.Jwt.TokenOptions;

namespace Saas.WebCoreApi;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {

            options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            }));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        }); 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContextFactory<GordionDbContext>();



        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            //(string)Convert.ChangeType(Configuration["TokenOptions:ValidAudience"], typeof(string)),
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

            };

        });
        builder.Services.AddDependencyResolvers(new ICoreModule[]
        {
            new CoreModule()
        });
        builder.Services.AddApiVersioning(options =>
        {
            // ReportApiVersions will return the "api-supported-versions" and "api-deprecated-versions" headers.
            options.ReportApiVersions = true;

            // Set a default version when it's not provided,
            // e.g., for backward compatibility when applying versioning on existing APIs
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);

            // Combine (or not) API Versioning Mechanisms:
            options.ApiVersionReader = ApiVersionReader.Combine(
                // The Default versioning mechanism which reads the API version from the "api-version" Query String paramater.
                new QueryStringApiVersionReader("api-version"),
                // Use the following, if you would like to specify the version as a custom HTTP Header.
                new HeaderApiVersionReader("Accept-Version"),
                // Use the following, if you would like to specify the version as a Media Type Header.
                new MediaTypeApiVersionReader("api-version")
            );
        });
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.1",
                Title = "Saas.WebCoreApi Doc.",
                Description = "Saas.WebCoreApi",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Sevda Ozdemir",
                    //Url = "",
                    Email = "yetissevda@gmail.com",
                },
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }

            });
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule(new AutofacBusinessModule());
            containerBuilder.RegisterType<GordionDbContext>().AsSelf().As<GordionDbContext>().InstancePerLifetimeScope();

        });

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();


        //debug ederken burayi kapatirsan hatalari kabak gibi gorebilirsin Sevda
        //app.ConfigureCustomExceptionMiddleware();

        app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseRouting();

        //neler yapilabilir.(evde ne yapilabilir) tokenla yetkiyi yakalmis olucaz
        app.UseAuthentication();

        //anahtar(eve giris) giris bilgileri ile login saglamak icin
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


        app.Run();
    }
}