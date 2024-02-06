using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioWebsite.Client;
using PortfolioWebsite.Client.Services;
using PortfolioWebsite.Client.Services.Contracts;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
	 .MinimumLevel
	.Information()
	.WriteTo
	.Console()
	.CreateLogger();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7240/") });

builder.Services.AddOidcAuthentication(options =>
{
	// Configure your authentication provider options here.
	// For more information, see https://aka.ms/blazor-standalone-auth
	builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
