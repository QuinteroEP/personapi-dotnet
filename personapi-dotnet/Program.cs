using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using personapi_dotnet.Interface;
using personapi_dotnet.Modules.Swagger; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ArqPerDbContext>(options =>
{
 options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});


var keysPath = Path.Combine(builder.Environment.ContentRootPath, "DataProtection-Keys");
Directory.CreateDirectory(keysPath);
builder.Services.AddDataProtection()
 .PersistKeysToFileSystem(new DirectoryInfo(keysPath))
 .SetApplicationName("personapi_dotnet_app");


builder.Services.AddScoped<personapi_dotnet.Repository.PersonaRepository>();
builder.Services.AddScoped<IPersonaRepository, personapi_dotnet.Repository.PersonaRepository>();
builder.Services.AddScoped<personapi_dotnet.Interface.EntityInterface<personapi_dotnet.Models.Entities.Persona>, personapi_dotnet.Repository.PersonaRepository>();

builder.Services.AddScoped<personapi_dotnet.Repository.ProfesionRepository>();
builder.Services.AddScoped<personapi_dotnet.Interface.EntityInterface<personapi_dotnet.Models.Entities.Profesion>, personapi_dotnet.Repository.ProfesionRepository>();

builder.Services.AddScoped<personapi_dotnet.Repository.EstudioRepository>();
builder.Services.AddScoped<personapi_dotnet.Interface.EntityInterface<personapi_dotnet.Models.Entities.Estudio>, personapi_dotnet.Repository.EstudioRepository>();

builder.Services.AddScoped<personapi_dotnet.Repository.TelefonoRepository>();
builder.Services.AddScoped<personapi_dotnet.Interface.EntityInterface<personapi_dotnet.Models.Entities.Telefono>, personapi_dotnet.Repository.TelefonoRepository>();

builder.Services.AddSwagger();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Modified V.2");
    c.RoutePrefix = "swagger";
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
