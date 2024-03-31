using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using APINotificacionesV2.Models.Datos;
using System.Net;
using APINotificacionesV2.Models.Repository.IRepository;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddScoped<IRepository<Usuarios>, UsuariosRepository>();
builder.Services.AddScoped<IRepository<Notas>, NotasRepository>();


// Inicio Conexion a la base
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("API"));
    options.LogTo(Console.WriteLine);
});
//Fin Conexion a la base

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();





// Configure the HTTPS endpoint with the desired SSL certificate
builder.WebHost.UseKestrel((context, options) =>
{
    options.Listen(IPAddress.Any, 7186, listenOptions =>
    {
        listenOptions.UseHttps("./cert.pfx", "4226");
    });
});






var app = builder.Build();







// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
// lo saque del condicional para que funcione en prod y desa
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
