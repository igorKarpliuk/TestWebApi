using System.Configuration;
using TestWebApi.Core.Interfaces;
using TestWebApi.Data.Implementations;
using TestWebApi.Data;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Services.Interfaces;
using TestWebApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IIncidentService, IncidentService>();
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
