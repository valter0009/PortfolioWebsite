
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web;
using Microsoft.AspNetCore.Components;
using PortfolioWebsite.App.Components;
using PortfolioWebsite.App.Services;
using PortfolioWebsite.App.Services.Contracts;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

Log.Logger = new LoggerConfiguration()
	 .MinimumLevel
	.Information()
	.WriteTo
	.Console()
	.CreateLogger();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddHxServices();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();



builder.Services.AddScoped(sp =>
{
	NavigationManager navigation = sp.GetRequiredService<NavigationManager>();
	return new HttpClient { BaseAddress = new Uri(navigation.BaseUri) };
}); //https://localhost:7240
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
