using AsistenciaApi.DTOs;
using AsistenciaApi.Models;
using System.Threading.Tasks;

namespace AsistenciaApi.Services
{
    public interface IUserService
    {
        Task<UsuarioDTO> RegisterUserAsync(Usuario usuario);
        Task<UsuarioDTO> LoginUserAsync(string nombre, string contrasena);

        // Nuevo método para validar credenciales
        Task<Usuario?> ValidarCredencialesAsync(string nombre, string contrasena);
    }
}
