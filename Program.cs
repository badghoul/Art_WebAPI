
using Auchan_WebAPI.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Auchan_WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using Microsoft.OpenApi.Any;
using static Auchan_WebAPI.ErrorResponse;
using Microsoft.AspNetCore.Mvc;


namespace Auchan_WebAPI
{
    /// <summary>
    /// enable the serialization and deserialization of DateTime objects with a specific date and time format
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public DateTimeConverter(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()??"", _format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            // builder pattern
            // constructs the web application host with all necessary configurations and services
            var builder = WebApplication.CreateBuilder(args);
            // configure the Dependency Injection container to include in-memory database context 'AuthDbContext'
            builder.Services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("AuthDb");
            });
            // add the ASP.NET Core Identity services to the DI container and expose API endpoints for Identity management
            // IdentityUser - default class provided by ASP.NET Core Identity to represent a user in the identity system
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                // 'AuthDbContext' is used to store the identity data (such as users, roles, claims, etc.)
                .AddEntityFrameworkStores<AuthDbContext>();
            // configure the DI container to use the MockArtRepo implementation for the IArtRepo interface and to use a singleton lifetime for this service
            builder.Services.AddSingleton<IArtRepo, MockArtRepo>();
            // configure the controllers and JSON serialization options
            builder.Services.AddControllers(config =>
            {
               
            }).AddJsonOptions(opt =>
            {
                // configure the JSON serializer to format the JSON output with indentation               
                opt.JsonSerializerOptions.WriteIndented = true;
                // handle the serialization and deserialization of enum values as strings
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                // handle the serialization and deserialization of DateTime values in format "dd.MM.yyyy HH:mm"
                opt.JsonSerializerOptions.Converters.Add(new DateTimeConverter("dd.MM.yyyy HH:mm"));
            });
            // add services required to generate metadata for API endpoints
            builder.Services.AddEndpointsApiExplorer();
            // configure Swagger
            builder.Services.AddSwaggerGen(opt =>
            {
                // customize how DateTime and nullable DateTime properties are represented in the Swagger documentation
                opt.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time", Example = new OpenApiString(DateTime.Now.ToString("dd.MM.yyyy HH:mm")) });
                opt.MapType<DateTime?>(() => new OpenApiSchema { Type = "string", Format = "date-time", Example = new OpenApiString(DateTime.Now.ToString("dd.MM.yyyy HH:mm")) });

                opt.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Auth",
                    Version = "v1"
                });
                // add security definition for JWT bearer token authentication
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                // define a security requirement for the API
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });
            // register custom exception handler middleware (centralized error handling, custom responses)
            builder.Services.AddExceptionHandler<ExceptionHandler>();
            // build the application host using the configuration provided in the WebApplicationBuilder
            var app = builder.Build();
            // conditional middleware configuration
            if (app.Environment.IsDevelopment())
            {
                // add middleware to the request pipeline to serve the Swagger JSON endpoint
                app.UseSwagger();
                // add middleware to the request pipeline to serve the Swagger UI
                app.UseSwaggerUI();
            }
            // enforce secure communication by redirecting requests from HTTP to HTTPS
            app.UseHttpsRedirection();
            // provide central place to handle exceptions that occur anywhere in the request pipeline
            app.UseExceptionHandler(_ => { });
            // return custom error pages or messages based on the HTTP status codes generated during request processing
            app.UseStatusCodePages();
            // configure and map API endpoints for identity operations such as user registration, login, password management, and more
            app.MapIdentityApi<IdentityUser>();
            // handle HTTP requests and route them to the appropriate controller actions
            app.MapControllers();
            // signal the end of the middleware configuration
            // start the web application
            // listening for incoming HTTP requests and processes them according to the configured middleware pipeline
            app.Run();
        }
    }
}
