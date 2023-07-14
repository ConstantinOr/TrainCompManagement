using Microsoft.EntityFrameworkCore;
using TrainCompManagement.DAL;
using TrainCompManagement.Domain;
using TrainCompManagement.Infrastructure.Query;
using TrainComponentManagementSystem;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITrainTreePathService, TrainTreePathService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllChildrenQuery).Assembly));

var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TrainCompManagementDbContext>(
    options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddGlobalErrorHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();