using Microsoft.EntityFrameworkCore;
using Pholium.Application.AutoMapper;
using Pholium.Data.Context;
using Pholium.IoC;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PholiumContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PholiumDB")).EnableSensitiveDataLogging());
builder.Services.AddAutoMapper(typeof(AutoMapperSetup));

IServiceCollection services = builder.Services;
NativeInjector.RegisterServices(services);



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
