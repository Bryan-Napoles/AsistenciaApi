using Microsoft.AspNetCore.Mvc;
using AsistenciaApi.Models;
using AsistenciaApi.DTOs;
using AsistenciaApi.Services;

namespace AsistenciaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint para registrar un nuevo usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Los datos del usuario son inválidos.");

            var result = await _userService.RegisterUserAsync(usuario);

            if (result == null)
                return BadRequest("El usuario ya está registrado.");

            return Ok(result);  // Retornamos el UsuarioDTO
        }

        // Endpoint para login de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Los datos del usuario son inválidos.");

            var result = await _userService.LoginUserAsync(usuario.Nombre, usuario.Contrasena);

            if (result == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(result);  // Retornamos el UsuarioDTO
        }

    }
}
