using AsistenciaApi.DTOs;
using AsistenciaApi.Models;

namespace AsistenciaApi.Services
{
    public interface IAsistenciaService
    {
        Task<Tuple<bool, RegistroAsistenciaDTO>> RegistrarAsistenciaAsync(RegistroAsistencia registro);
    }
}

