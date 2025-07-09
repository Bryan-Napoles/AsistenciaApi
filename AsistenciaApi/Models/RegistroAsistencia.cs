using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AsistenciaApi.Models
{
    public class RegistroAsistencia
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public TimeSpan? HoraEntradaComida { get; set; }
        public TimeSpan? HoraSalidaComida { get; set; }
        public bool PermisoEntradaTarde { get; set; }
        public bool PermisoSalidaTemprana { get; set; }
    }
}
