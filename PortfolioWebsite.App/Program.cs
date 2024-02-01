using Azure.Communication.Email;
using Havit.Blazor.Components.Web;
using PortfolioWebsite.Api.Repositories;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.App.Components;
using PortfolioWebsite.App.Services;
using PortfolioWebsite.App.Services.Contracts;
using PortfolioWebsite.App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHxServices();

builder.Services.AddSingleton<EmailClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var emailSettings = configuration.GetSection("EmailSettings");
    var endpoint = emailSettings["ConnectionString"];
    var connectionString = endpoint;
    return new EmailClient(connectionString);
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7240/") });
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
