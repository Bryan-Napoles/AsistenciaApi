using System;
using System.Xml.Linq;

namespace AsistenciaApi.Models
{
    public class RegistroAsistencia
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? Fecha { get; set; }  // Modificado a DateTime? para permitir valores nulos
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public TimeSpan? HoraEntradaComida { get; set; }
        public TimeSpan? HoraSalidaComida { get; set; }
        public bool PermisoEntradaTarde { get; set; }
        public bool PermisoSalidaTemprana { get; set; }



    // Constructor opcional si necesitas inicializar valores por defecto
    public RegistroAsistencia()
    {
        Fecha = DateTime.Now; // Fecha predeterminada al momento de la creación
        HoraEntrada = null;
        HoraSalida = null;
        HoraEntradaComida = null;
        HoraSalidaComida = null;
    }
}
}
