using AsistenciaApi.DTOs;
using AsistenciaApi.Models;
using AsistenciaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciaController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarAsistencia([FromBody] RegistroAsistenciaDTO dto)
        {
            if (dto.UsuarioId <= 0)
                return BadRequest("Debe proporcionar un ID de usuario válido.");

            var registro = new RegistroAsistencia
            {
                UsuarioId = dto.UsuarioId,
                Fecha = dto.Fecha,
                HoraEntrada = dto.HoraEntrada,
                HoraSalida = dto.HoraSalida,
                HoraEntradaComida = dto.HoraEntradaComida,
                HoraSalidaComida = dto.HoraSalidaComida,
                PermisoEntradaTarde = dto.PermisoEntradaTarde,
                PermisoSalidaTemprana = dto.PermisoSalidaTemprana
            };

            var result = await _asistenciaService.RegistrarAsistenciaAsync(registro);

            if (!result.Item1)
                return BadRequest("No se pudo registrar la asistencia.");

            return Ok(result.Item2);
        }


        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerRegistrosPorUsuario(int usuarioId)
        {
            var registros = await _asistenciaService.ObtenerRegistrosPorUsuarioId(usuarioId);
            return Ok(registros);
        }

        // ✅ Nuevo endpoint: resumen por rango de fechas
        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumenAsistencia(int usuarioId, DateTime desde, DateTime hasta)
        {
            try
            {
                // Log para ver qué valores estamos recibiendo en la solicitud
                Console.WriteLine($"Obteniendo resumen de asistencia para el usuarioId {usuarioId} desde {desde} hasta {hasta}");

                // Llamada al servicio para obtener los registros
                var resumen = await _asistenciaService.ObtenerResumenPorUsuarioAsync(usuarioId, desde, hasta);

                if (resumen == null)
                {
                    Console.WriteLine("Resumen no encontrado.");
                    return NotFound("No se encontró el resumen.");
                }

                Console.WriteLine($"Resumen encontrado: Total Asistencias: {resumen.TotalAsistencias}");

                return Ok(resumen);
            }
            catch (Exception ex)
            {
                // Imprimir el error completo en la consola
                Console.WriteLine($"Error al obtener el resumen: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                // Retornar error interno al cliente
                return StatusCode(500, "Error interno en el servidor");
            }
        }



    }
}
