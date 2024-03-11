using BalanceAPI.Services;
using Microsoft.EntityFrameworkCore;
using TopUpDB.Entity;
using TopUpDB.Implementation;
using TopUpDB.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    o.UseSqlite(connectionString);
});

builder.Services.AddScoped<IBalance, BalanceImplementation>();
builder.Services.AddScoped<IBalanceService, BalanceService>();


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
