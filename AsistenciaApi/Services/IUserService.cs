using AsistenciaApi.DTOs;
using AsistenciaApi.Models;

namespace AsistenciaApi.Services
{
    public interface IUserService
    {
        Task<UsuarioDTO> RegisterUserAsync(Usuario usuario);
        Task<UsuarioDTO> LoginUserAsync(string nombre, string contrasena);
    }
}
