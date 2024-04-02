using Microsoft.EntityFrameworkCore;
using APINotificacionesV2.Models.Datos;
using System.Net;
using APINotificacionesV2.Models.Repository.IRepository;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Repository.Implementations;
using Amazon;
using APINotificacionesV2.Configuration;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IRepository<Usuarios>, UsuariosRepository>();
builder.Services.AddScoped<IRepository<Notas>, NotasRepository>();

// Obtener la cadena de conexión de aws.

var env = builder.Environment.EnvironmentName;
var appName = builder.Environment.ApplicationName;
builder.Configuration.AddSecretsManager(region: RegionEndpoint.USEast1,
    configurator: options => {
        options.SecretFilter = entry => entry.Name.StartsWith($"{env}_{appName}_");
        options.KeyGenerator = (entry, s) => s
        .Replace($"{env}_{appName}_", string.Empty)
        .Replace("__", ":");
});



// Inicio Conexion a la base
//configuro la conexion usando la clase DataseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(DatabaseSettings.SectionName));
builder.Services.AddDbContext<DBContext>((serviceProvider, options) =>
{
    //Resuelve DatabaseSettings del ServiceProvider
    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    //Configura el DbContext con la cadena de conexión
    options.UseSqlServer(databaseSettings.ConnectionString);
    options.LogTo(Console.WriteLine);
});

//Fin Conexion a la base

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


 


//lo saque del condicional para que funcione en prod y desa
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Habilitar CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});



app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
