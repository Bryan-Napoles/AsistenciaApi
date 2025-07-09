using AsistenciaApi.Models;
using AsistenciaApi.Data;
using AsistenciaApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaApi.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly AsistenciaDbContext _context;

        public AsistenciaService(AsistenciaDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<bool, RegistroAsistenciaDTO>> RegistrarAsistenciaAsync(RegistroAsistencia registro)
        {
            try
            {
                Console.WriteLine(">>> Iniciando registro de asistencia...");
                Console.WriteLine($"UsuarioId recibido: {registro.UsuarioId}");
                Console.WriteLine($"Fecha: {registro.Fecha}");
                Console.WriteLine($"HoraEntrada: {registro.HoraEntrada}");
                Console.WriteLine($"HoraSalida: {registro.HoraSalida}");

                var usuario = await _context.Usuarios.FindAsync(registro.UsuarioId);

                if (usuario == null)
                {
                    Console.WriteLine(">>> Usuario no encontrado en la base de datos.");
                    return new Tuple<bool, RegistroAsistenciaDTO>(false, null!);
                }

                // Asegurarse de que la fecha esté en UTC
                registro.Fecha = registro.Fecha == default
                    ? DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
                    : DateTime.SpecifyKind(registro.Fecha, DateTimeKind.Utc);

                // Para las horas, mantén las horas como TimeSpan
                registro.HoraEntrada = registro.HoraEntrada ?? TimeSpan.Zero;
                registro.HoraSalida = registro.HoraSalida ?? TimeSpan.Zero;
                registro.HoraEntradaComida = registro.HoraEntradaComida ?? TimeSpan.Zero;
                registro.HoraSalidaComida = registro.HoraSalidaComida ?? TimeSpan.Zero;

                _context.RegistrosAsistencia.Add(registro);
                await _context.SaveChangesAsync();

                var dto = new RegistroAsistenciaDTO
                {
                    UsuarioId = registro.UsuarioId,
                    Fecha = registro.Fecha,
                    HoraEntrada = registro.HoraEntrada,
                    HoraSalida = registro.HoraSalida,
                    HoraEntradaComida = registro.HoraEntradaComida,
                    HoraSalidaComida = registro.HoraSalidaComida,
                    PermisoEntradaTarde = registro.PermisoEntradaTarde,
                    PermisoSalidaTemprana = registro.PermisoSalidaTemprana
                };

                Console.WriteLine(">>> Registro de asistencia exitoso.");
                return new Tuple<bool, RegistroAsistenciaDTO>(true, dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> Error al registrar asistencia:");
                Console.WriteLine(ex.ToString());
                return new Tuple<bool, RegistroAsistenciaDTO>(false, null!);
            }
        }

        public async Task<List<RegistroAsistencia>> ObtenerRegistrosPorUsuarioId(int usuarioId)
        {
            return await _context.RegistrosAsistencia
                                 .Where(r => r.UsuarioId == usuarioId)
                                 .ToListAsync();
        }

        // Resumen de asistencia
        public async Task<ResumenAsistenciaDTO> ObtenerResumenPorUsuarioAsync(int usuarioId, DateTime desde, DateTime hasta)
        {
            try
            {
                // Asegurar que las fechas estén en UTC
                desde = DateTime.SpecifyKind(desde, DateTimeKind.Utc);
                hasta = DateTime.SpecifyKind(hasta, DateTimeKind.Utc);

                // Imprimir los parámetros recibidos para depuración
                Console.WriteLine($">>> Parámetros recibidos:");
                Console.WriteLine($"UsuarioId: {usuarioId}, Desde: {desde}, Hasta: {hasta}");

                var registros = await _context.RegistrosAsistencia
                    .Where(r => r.UsuarioId == usuarioId && r.Fecha >= desde && r.Fecha <= hasta)
                    .ToListAsync();

                // Verificar si hay registros y qué registros estamos obteniendo
                Console.WriteLine($">>> Registros encontrados: {registros.Count}");

                return new ResumenAsistenciaDTO
                {
                    TotalAsistencias = registros.Count,
                    TotalRetardos = registros.Count(r => r.PermisoEntradaTarde),
                    TotalSalidasTempranas = registros.Count(r => r.PermisoSalidaTemprana),
                    TotalPermisosEspeciales = registros.Count(r => r.PermisoEntradaTarde || r.PermisoSalidaTemprana),
                    Desde = desde,
                    Hasta = hasta
                };
            }
            catch (Exception ex)
            {
                // En caso de que haya un error, imprimirlo en consola
                Console.WriteLine($">>> Error en ObtenerResumenPorUsuarioAsync: {ex.Message}");
                throw;  // Lanza la excepción para ser manejada en el controlador
            }
        }
    }
}
