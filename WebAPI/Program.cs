using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Middlewares;
using Core;
using Core.DependencyResolvers.Autofac;
using Infrastructure;
using Core.Utilities;
using DataAccess;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Service'lerin kullanımı için yazmış olduğumuz extension method'lar;
builder.Services.RegisterWebApiServices();
builder.Services.RegisterCoreServices();
builder.Services.RegisterInfrastructureServices();
builder.Services.RegisterDataAccessServices();
builder.Services.RegisterBusinessServices();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// IoC container'a eklenen tüm service'lere global olarak erişmek için ServicesTool'umuza IServiceCollection'ı gönderiyoruz.
ServicesTool.CreateServiceProvider(builder.Services);


var app = builder.Build();

// Autofac container' a eklenen tüm service'lere global olarak erişmek için ILifetimeScope'u ServiceTool'umuza gönderiyoruz.
ServicesTool.CreateAutofacServiceProvider(app.Services.GetAutofacRoot());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


// Custom exception middleware
app.UseCustomExceptionHandler();

app.MapControllers();

// Eğer ki varsa yeni eklenen migration onları otomatik olarak Db'ye yansıtacak. (Manuel Update-Database'e son! Halı, kilim 5dk da dikilir, hemen teslim edilir.)
await ServicesTool.GetService<NADbContext>().Database.MigrateAsync();

BusinessRoleManager.RegisterBusinessRoles();

app.Run();
