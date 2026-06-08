using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Folio.Application.Interfaces;
using Folio.Application.Services;
using Folio.Core.Interfaces;
using Folio.Infrastructure.Data;
using Folio.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration["AzureSql:ConnectionString"]));
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

builder.Services.AddSingleton(_ =>
    new BlobServiceClient(builder.Configuration["AzureStorage:ConnectionString"]));
builder.Services.AddScoped<IFileStorageService>(sp => new AzureBlobStorageService(
    sp.GetRequiredService<BlobServiceClient>(),
    builder.Configuration["AzureStorage:ContainerName"]!));

builder.Services.AddSingleton(_ =>
    new ServiceBusClient(builder.Configuration["AzureServiceBus:ConnectionString"]));
builder.Services.AddScoped<IMessageQueueService>(sp => new AzureServiceBusService(
    sp.GetRequiredService<ServiceBusClient>(),
    builder.Configuration["AzureServiceBus:QueueName"]!));

builder.Services.AddScoped<IUploadFileService, UploadFileService>();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
