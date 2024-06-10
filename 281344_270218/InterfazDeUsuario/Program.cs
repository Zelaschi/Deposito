using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Repository;
using Repository.SQL;
using BusinessLogic;
using Domain;
using ControllerLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContextFactory<DepositoContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        providerOptions => providerOptions.EnableRetryOnFailure())
    , ServiceLifetime.Singleton);


builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<DepositoRepository>();
builder.Services.AddScoped<PromocionRepository>();
builder.Services.AddScoped<AdministradorRepository>();
builder.Services.AddScoped<ReservaRepository>();

builder.Services.AddScoped<ClienteLogic>();
builder.Services.AddScoped<DepositoLogic>();
builder.Services.AddScoped<PromocionLogic>();
builder.Services.AddScoped<AdministradorLogic>();
builder.Services.AddScoped<ReservaLogic>();
builder.Services.AddScoped<Controller>();

builder.Services.AddSingleton<DTOSesion>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
