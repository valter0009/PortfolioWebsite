var builder = WebAssemblyHostBuilder.CreateDefault(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Using httpclient to make requests to the server, utilizing httpclientfactory to create authorized and unauthorized clients
builder.Services.AddHttpClient("AnonymousClient",
    client => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

builder.Services
    .AddHttpClient("AuthorizedClient", client => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); })
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJsInteropService, JsInteropService>();

// Add Auth0 authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
    options.ProviderOptions.DefaultScopes.Add("openid email profile ");
});

await builder.Build().RunAsync();