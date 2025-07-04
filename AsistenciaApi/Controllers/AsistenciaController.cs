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
            // Validar si el modelo tiene los datos completos (sin campos vacíos)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Si el modelo no es válido, devolvemos un BadRequest con los detalles
            }

            var result = await _asistenciaService.RegistrarAsistenciaAsync(registro);

            if (!result.Item1)
            {
                return BadRequest("El usuario no existe o ocurrió un error al registrar la asistencia.");
            }

            // Si el usuario existe y el registro es exitoso, devolvemos el DTO con el registro de asistencia creado
            return Ok(result.Item2); // Retorna el RegistroAsistenciaDTO
        }

    }
}
