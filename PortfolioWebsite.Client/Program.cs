using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioWebsite.Client;
using PortfolioWebsite.Client.Services;
using PortfolioWebsite.Client.Services.Contracts;
using Serilog;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

// Add root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register local storage services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();


builder.Services.AddHttpClient("AnonymousClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
    options.ProviderOptions.DefaultScopes.Add("openid email profile ");
});

await builder.Build().RunAsync();