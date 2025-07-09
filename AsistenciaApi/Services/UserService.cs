using AsistenciaApi.Models;
using AsistenciaApi.Data;
using AsistenciaApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AsistenciaApi.Services
{
    public class UserService : IUserService
    {
        private readonly AsistenciaDbContext _context;

        public UserService(AsistenciaDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO> RegisterUserAsync(Usuario usuario)
        {
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == usuario.Nombre &&
                                          u.ApellidoPaterno == usuario.ApellidoPaterno &&
                                          u.ApellidoMaterno == usuario.ApellidoMaterno);
            if (existingUser != null)
                return null;

            usuario.Contrasena = EncryptPassword(usuario.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno
            };
        }

        public async Task<UsuarioDTO> LoginUserAsync(string nombre, string contrasena)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre);

            if (user == null || !VerifyPassword(contrasena, user.Contrasena))
                return null;

            return new UsuarioDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                ApellidoPaterno = user.ApellidoPaterno,
                ApellidoMaterno = user.ApellidoMaterno
            };
        }

        // ✅ Método requerido por AuthController
        public async Task<Usuario?> ValidarCredencialesAsync(string nombre, string contrasena)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre);

            if (user == null || !VerifyPassword(contrasena, user.Contrasena))
                return null;

            return user;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var enteredHash = EncryptPassword(enteredPassword);
            return enteredHash == storedPassword;
        }

        private string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
