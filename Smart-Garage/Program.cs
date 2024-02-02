using Microsoft.EntityFrameworkCore;
using Smart_Garage.Helpers.Contracts;
using Smart_Garage.Helpers;
using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Repositories;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ForumManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Repository
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            // Service
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IVehicleService, VehicleRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();

            // AuthManager
            builder.Services.AddScoped<AuthManager>();

            // Mapper
            builder.Services.AddScoped<IModelMapper, ModelMapper>();
            builder.Services.AddAutoMapper(typeof(Program));
            AutoMapper.IConfigurationProvider cfg = new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfiles>(); });
            builder.Services.AddSingleton(cfg);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddDbContext<SGContext>(options =>
            {
                // A connection string for establishing a connection to the locally installed SQL Server.
                // Set serverName to your local instance; databaseName is the name of the database
                string connectionString = @"Server=localhost\SQLEXPRESS;Database=SmartGarage;Trusted_Connection=True;";
                //string connectionString = @"Server=localhost;Database=ForumManagementSystem;Trusted_Connection=True;";
                // Configure the application to use the locally installed SQL Server.
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}


    