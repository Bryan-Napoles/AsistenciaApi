using AsistenciaApi.Data;
using AsistenciaApi.DTOs;
using AsistenciaApi.Models;
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
                // Agregar un log para verificar los datos del usuario
                Console.WriteLine($"Buscando usuario con Nombre: {registro.Nombre}, ApellidoPaterno: {registro.ApellidoPaterno}, ApellidoMaterno: {registro.ApellidoMaterno}");

                // Buscar al usuario por Nombre, ApellidoPaterno y ApellidoMaterno
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Nombre == registro.Nombre &&
                                              u.ApellidoPaterno == registro.ApellidoPaterno &&
                                              u.ApellidoMaterno == registro.ApellidoMaterno);

                // Si el usuario no existe, devolver false y un DTO nulo
                if (usuario == null)
                {
                    Console.WriteLine($"Usuario con Nombre: {registro.Nombre}, ApellidoPaterno: {registro.ApellidoPaterno}, ApellidoMaterno: {registro.ApellidoMaterno} no encontrado.");
                    return new Tuple<bool, RegistroAsistenciaDTO>(false, null); // Usuario no encontrado
                }

                // Asignar la hora actual del sistema a la hora de entrada si no se proporciona
                if (registro.HoraEntrada == TimeSpan.Zero)
                {
                    registro.HoraEntrada = DateTime.Now.TimeOfDay; // Hora actual del sistema
                }

                // Asignar la hora actual del sistema a la hora de salida si no se proporciona
                if (registro.HoraSalida == TimeSpan.Zero)
                {
                    registro.HoraSalida = DateTime.Now.TimeOfDay; // Hora actual del sistema
                }

                // Registrar la asistencia
                _context.RegistrosAsistencia.Add(registro);
                await _context.SaveChangesAsync();

                // Mapeo manual a DTO
                var registroDTO = new RegistroAsistenciaDTO
                {
                    Nombre = registro.Nombre,
                    ApellidoPaterno = registro.ApellidoPaterno,
                    ApellidoMaterno = registro.ApellidoMaterno,
                    Fecha = registro.Fecha,
                    HoraEntrada = registro.HoraEntrada,
                    HoraSalida = registro.HoraSalida,
                    HoraEntradaComida = registro.HoraEntradaComida,
                    HoraSalidaComida = registro.HoraSalidaComida,
                    PermisoEntradaTarde = registro.PermisoEntradaTarde,
                    PermisoSalidaTemprana = registro.PermisoSalidaTemprana
                };

                // Retornar el Tuple con 'true' y el DTO
                Console.WriteLine($"Asistencia registrada para el usuario con Nombre: {registro.Nombre}, ApellidoPaterno: {registro.ApellidoPaterno}, ApellidoMaterno: {registro.ApellidoMaterno}.");
                return new Tuple<bool, RegistroAsistenciaDTO>(true, registroDTO);
            }
            catch (Exception ex)
            {
                // Si hay un error, lo registramos
                Console.WriteLine($"Error al registrar la asistencia: {ex.Message}");
                return new Tuple<bool, RegistroAsistenciaDTO>(false, null);
            }
        }

    }
}

