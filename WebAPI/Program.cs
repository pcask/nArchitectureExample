﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Middlewares;
using Business;
using Business.DependencyResolvers.Autofac;
using Core;
using Core.Utilities;
using DataAccess;
using DataAccess.Contexts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.RegisterCoreServices();
//builder.Services.RegisterInfrastructureServices();
builder.Services.RegisterDataAccessServices();
builder.Services.RegisterBusinessServices();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


// IoC container'a eklenen tüm service'lere global olarak erişmek için ServicesTool'umuza IServiceCollection'ı gönderiyoruz.
ServicesTool.CreateServiceProvider(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Custom exception middleware
app.UseCustomExceptionHandler();

app.MapControllers();

// Eğer ki varsa yeni eklenen migration onları otomatik olarak Db'ye yansıtacak. (Manuel Update-Database'e son! İşçiyiz haklıyız asvfajs)
await ServicesTool.GetService<NADbContext>().Database.MigrateAsync();

app.Run();
