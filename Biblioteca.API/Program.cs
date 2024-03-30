using Biblioteca.BL.Contract;
using Biblioteca.BL.Services;
using Biblioteca.DAL.Context;
using Biblioteca.DAL.Interfaces;
using Biblioteca.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BibliotecaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaContext")));

//Repositories
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ILibroRepository, LibroRepository>();
builder.Services.AddTransient<IPrestamoRepository, PrestamoRepository>();
//Services
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<ILibroService, LibroService>();
builder.Services.AddTransient<IPrestamoService, PrestamoService>();

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
