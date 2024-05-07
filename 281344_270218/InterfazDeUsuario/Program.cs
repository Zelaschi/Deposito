using InterfazDeUsuario.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Repository;
using BusinessLogic;
using Domain;
using ControllerLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddSingleton<IRepository<Cliente>, ClienteMemoryRepository>();
builder.Services.AddSingleton<IRepository<Deposito>, DepositoMemoryRepository>();
builder.Services.AddSingleton<IRepository<Promocion>, PromocionMemoryRepository>();
builder.Services.AddSingleton<IRepository<Administrador>, AdministradorMemoryRepository>();
builder.Services.AddSingleton<IRepository<Reserva>, ReservaMemoryRepository>();

builder.Services.AddSingleton<ClienteLogic>();
builder.Services.AddSingleton<DepositoLogic>();
builder.Services.AddSingleton<PromocionLogic>();
builder.Services.AddSingleton<AdministradorLogic>();
builder.Services.AddSingleton<ReservaLogic>();
builder.Services.AddSingleton<SessionLogic>();
builder.Services.AddSingleton<Controller>();


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
