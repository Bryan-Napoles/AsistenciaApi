using AsistenciaApi.DTOs;
using AsistenciaApi.Models;

namespace AsistenciaApi.Services
{
    public interface IAsistenciaService
    {
        Task<Tuple<bool, RegistroAsistenciaDTO>> RegistrarAsistenciaAsync(RegistroAsistencia registro);
        Task<List<RegistroAsistencia>> ObtenerRegistrosPorUsuarioId(int usuarioId);
        Task<ResumenAsistenciaDTO> ObtenerResumenPorUsuarioAsync(int usuarioId, DateTime desde, DateTime hasta);

    }
}
