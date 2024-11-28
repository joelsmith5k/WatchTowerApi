using Microsoft.EntityFrameworkCore;
using WatchTowerApi.Models;
using WatchTowerApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<DbHealthCheckService>();

var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"Current Environment: {environment}");

builder.Services.AddDbContext<WatchTowerContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://gray-dune-016edb11e.5.azurestaticapps.net")  // Allow this origin
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithMethods("GET", "POST", "OPTIONS");
    });
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

using (var scope = app.Services.CreateScope())
{
    var dbHealthCheck = scope.ServiceProvider.GetRequiredService<DbHealthCheckService>();
    await dbHealthCheck.CheckConnectionAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
