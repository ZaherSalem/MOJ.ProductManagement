using Microsoft.OpenApi.Models;
using MOJ.ProductManagement.Application.Extensions;
using MOJ.ProductManagement.Infrastructure.Data.Seed;
using MOJ.ProductManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddApplicationLayer(builder.Configuration);

// Configure Swagger to generate documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Management API",
        Version = "v1",
        Description = "API for managing products and suppliers"
    });
});

 //CORS
 builder.Services.AddCors(options =>
 {
     options.AddPolicy("AllowAll", builder =>
     {
         builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
     });
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
 if (app.Environment.IsDevelopment())
 {
     // Make Swagger the default page
     app.UseSwagger();
     app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API V1");
         c.RoutePrefix = string.Empty; // Set Swagger UI at the root URL
     });
 }

  //CORS
 app.UseCors("AllowAll");

// Seed the database during application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Seed data
    var seeder = services.GetRequiredService<DatabaseInitializer>();
    await seeder.Initialize();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
