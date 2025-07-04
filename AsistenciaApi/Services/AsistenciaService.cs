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

        // Método para registrar la asistencia
        public async Task<Tuple<bool, RegistroAsistenciaDTO>> RegistrarAsistenciaAsync(RegistroAsistencia registro)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _context.Usuarios
    .FirstOrDefaultAsync(u => u.Id == registro.UsuarioId);



                // Si el usuario existe, asignar los datos de la asistencia
                // Ya no necesitamos asignar el UsuarioId, ya viene de la solicitud

                // Verificar si la fecha es nula antes de intentar asignarla
                if (registro.Fecha == null)
                {
                    // Asignar la fecha actual si no se proporcionó
                    registro.Fecha = DateTime.Now;
                }

                // Registrar la asistencia en la base de datos
                _context.RegistrosAsistencia.Add(registro);
                await _context.SaveChangesAsync();

                // Crear el DTO para devolver con los datos del registro de asistencia
                var registroDTO = new RegistroAsistenciaDTO
                {
                    UsuarioId = registro.UsuarioId,  // Retornamos solo el UsuarioId
                    Fecha = registro.Fecha.Value,    // Asegúrate de acceder al valor de DateTime?
                    HoraEntrada = registro.HoraEntrada,
                    HoraSalida = registro.HoraSalida,
                    HoraEntradaComida = registro.HoraEntradaComida,
                    HoraSalidaComida = registro.HoraSalidaComida,
                    PermisoEntradaTarde = registro.PermisoEntradaTarde,
                    PermisoSalidaTemprana = registro.PermisoSalidaTemprana
                };

                // Devolver el resultado con 'true' y el DTO
                return new Tuple<bool, RegistroAsistenciaDTO>(true, registroDTO);
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error: {ex.Message}");

                // Retornar el resultado del error
                return new Tuple<bool, RegistroAsistenciaDTO>(false, null);
            }
        }
    }
}
