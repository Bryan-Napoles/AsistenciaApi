using Microsoft.AspNetCore.Mvc;
using AsistenciaApi.Models;
using AsistenciaApi.Services;

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
        public async Task<IActionResult> RegistrarAsistencia([FromBody] RegistroAsistencia registro)
        {
            // Verificar que los campos requeridos no sean nulos o vacíos
            if (registro.UsuarioId == 0 || registro.Fecha == null)
            {
                return BadRequest("El UsuarioId o la fecha son requeridos.");
            }

            // Registrar la asistencia
            var result = await _asistenciaService.RegistrarAsistenciaAsync(registro);

            if (!result.Item1)
            {
                return BadRequest("El usuario no existe o ocurrió un error al registrar la asistencia.");
            }

            // Solo retornamos el UsuarioId, Fecha y las horas de entrada y salida
            return Ok(new
            {
                UsuarioId = result.Item2.UsuarioId,
                Fecha = result.Item2.Fecha,
                HoraEntrada = result.Item2.HoraEntrada,
                HoraSalida = result.Item2.HoraSalida,
                HoraEntradaComida = result.Item2.HoraEntradaComida,
                HoraSalidaComida = result.Item2.HoraSalidaComida,
                PermisoEntradaTarde = result.Item2.PermisoEntradaTarde,
                PermisoSalidaTemprana = result.Item2.PermisoSalidaTemprana
            });
        }

    }
}
