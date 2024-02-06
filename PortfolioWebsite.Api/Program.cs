using Azure.Communication.Email;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Repositories;
using PortfolioWebsite.Api.Repositories.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Log.Logger = new LoggerConfiguration()
     .MinimumLevel
    .Information()
    .WriteTo
    .Console()
    .CreateLogger();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}"
        };
    });


if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");

    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());

    builder.Services.AddDbContext<PortfolioWebsiteDbContext>(options =>
    {

        options.UseSqlServer(client.GetSecret("AzureSqlConnectionString").Value.Value.ToString());
    });

    var stripeApiKey = client.GetSecret("StripeApiKey").Value.Value;
    var stripeEndpointSecret = client.GetSecret("StripeEndpoindScrt").Value.Value;
    builder.Configuration["StripeApiKey"] = stripeApiKey;
    builder.Configuration["StripeEndpoindScrt"] = stripeEndpointSecret;



    builder.Services.AddSingleton<EmailClient>(sp =>
    {

        var configuration = sp.GetRequiredService<IConfiguration>();
        var emailSettings = configuration.GetSection("EmailSettings");
        var connectionString = client.GetSecret("EmailConnectionString").Value.Value.ToString();

        return new EmailClient(connectionString);
    });
}
/*if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<EmailClient>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var emailSettings = configuration.GetSection("EmailSettingsDevelopment");
        var endpoint = emailSettings["ConnectionString"];
        var connectionString = endpoint;
        return new EmailClient(connectionString);
    });

    builder.Services.AddDbContext<PortfolioWebsiteDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnectionString"));
    });

}*/

builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}


app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();


app.MapFallbackToFile("index.html");

app.Run();
