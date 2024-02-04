using Azure.Communication.Email;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSingleton<EmailClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var emailSettings = configuration.GetSection("EmailSettings");
    var endpoint = emailSettings["ConnectionString"];
    var connectionString = endpoint;
    return new EmailClient(connectionString);
});




builder.Services
    .AddDbContextPool<PortfolioWebsiteDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlServerString"))
);
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
