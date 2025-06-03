using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PicPaySimplified.Infrastructure;
using PicPaySimplified.Infrastructure.Repositories.Transfer;
using PicPaySimplified.Infrastructure.Repositories.Wallet;
using PicPaySimplified.Services.Authorizer;       
using PicPaySimplified.Services.Notification;
using PicPaySimplified.Services.Transfer;
using PicPaySimplified.Services.Validator;
using PicPaySimplified.Services.Wallet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddHttpClient<IAuthorizerService, AuthorizerService>();


builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITransferValidator, TransferValidator>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PicPay Simplified API",
        Version = "v1",
        Description = "Documentação da API de transferências e carteiras do projeto PicPaySimplified"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PicPay Simplified API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
