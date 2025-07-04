using AsistenciaApi.Data;
using AsistenciaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio IUserService con su implementación UserService
builder.Services.AddScoped<IUserService, UserService>();

// Registrar el servicio IAsistenciaService con su implementación AsistenciaService
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();

// Registrar DbContext con PostgreSQL
builder.Services.AddDbContext<AsistenciaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// Registrar controladores y otras configuraciones
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
