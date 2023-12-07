using DUNPLab.API.Infrastructure;
using DUNPLab.API.ServiceInterfaces;
using DUNPLab.API.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DunpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<ITransferRezultati, TransferRezultati>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // U slucaju kruzne reference, resava problem.
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseHangfireServer();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<ITransferRezultati>("transfer-rezultata", service => service.Transfer(), "*/5 * * * *");

app.MapControllers();

app.Run();
