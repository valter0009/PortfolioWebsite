using Azure.Communication.Email;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



if (builder.Environment.IsDevelopment())
{
    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");

    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());

    var stripeApiKey = client.GetSecret("StripeApiKey").Value.Value;
    var stripeEndpointSecret = client.GetSecret("StripeEndpoindScrt").Value.Value;
    builder.Configuration["StripeApiKey"] = stripeApiKey;
    builder.Configuration["StripeEndpoindScrt"] = stripeEndpointSecret;

    builder.Services.AddDbContext<PortfolioWebsiteDbContext>(options =>
    {
        options.UseSqlServer(client.GetSecret("AzureSqlConnectionString").Value.Value.ToString());
    });

    builder.Services.AddSingleton<EmailClient>(sp =>
    {

        var configuration = sp.GetRequiredService<IConfiguration>();
        var emailSettings = configuration.GetSection("EmailSettingsProduction");
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
