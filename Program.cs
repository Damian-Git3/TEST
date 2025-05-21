using System.Data.Common;
using Microsoft.Data.SqlClient; // Import the appropriate namespace for your database provider
using DotNetEnv;
using TEST.Repositories;

var builder = WebApplication.CreateBuilder(args);

/* CARGA DE VARIABLES DE ENTORNO */
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddSingleton<TEST.Shared.DbConnection>(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    return new TEST.Shared.DbConnection(connectionString);
});

// Add services to the container.
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddControllers();

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

app.MapControllers();

app.Run();
