using Azure.Communication.Email;
using Havit.Blazor.Components.Web;
using PortfolioWebsite.App.Components;
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
