using Microsoft.EntityFrameworkCore;
using Pholium.Application.AutoMapper;
using Pholium.Data.Context;
using Pholium.IoC;
using AutoMapper;
using Pholium.Swagger;
using System.Text;
using Pholium.Auth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
                                    {
                                        options.AddPolicy(name: MyAllowSpecificOrigins,
                                                          policy =>
                                                          {
                                                              policy.WithOrigins("https://localhost:44437",
                                                                                  "http://localhost:44437");
                                                          });
                                    });
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PholiumContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PholiumDB")).EnableSensitiveDataLogging());
builder.Services.AddAutoMapper(typeof(AutoMapperSetup));
builder.Services.AddSwaggerConfiguration();

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
                                   {
                                       x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                       x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                   }
                                   ).AddJwtBearer(x =>
                                                   {
                                                       x.RequireHttpsMetadata = false;
                                                       x.SaveToken = true;
                                                       x.TokenValidationParameters = new TokenValidationParameters
                                                       {
                                                           ValidateIssuerSigningKey = true,
                                                           IssuerSigningKey = new SymmetricSecurityKey(key),
                                                           ValidateIssuer = false,
                                                           ValidateAudience = false
                                                       };
                                                   }
                                   );

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
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerConfigutarion();

app.MapControllerRoute(
                       name: "default",
                       pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();