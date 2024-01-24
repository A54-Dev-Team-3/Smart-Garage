using Microsoft.EntityFrameworkCore;
using Smart_Garage.Models;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();