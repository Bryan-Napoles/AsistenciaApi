﻿using AsistenciaApi.Services;
using Microsoft.AspNetCore.Mvc;
using AsistenciaApi.Models;
using AsistenciaApi.DTOs;


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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            var nuevoUsuario = await _userService.RegisterUserAsync(usuario);

            if (nuevoUsuario == null)
                return BadRequest("El usuario ya existe.");

            return Ok(new
            {
                mensaje = "Registro exitoso",
                usuarioId = nuevoUsuario.Id,
                nombre = nuevoUsuario.Nombre
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            var usuario = await _userService.ValidarCredencialesAsync(loginDto.Nombre, loginDto.Contrasena);

            if (usuario == null)
                return Unauthorized("Nombre o contraseña incorrectos.");

            // Personalizamos el nombre del campo a usuarioId
            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso",
                usuarioId = usuario.Id,
                nombre = usuario.Nombre
            });
        }

    }
}
