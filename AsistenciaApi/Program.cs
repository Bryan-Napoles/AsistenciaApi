using AsistenciaApi.Data;
using AsistenciaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();

// DbContext con PostgreSQL
builder.Services.AddDbContext<AsistenciaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// Controladores
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
