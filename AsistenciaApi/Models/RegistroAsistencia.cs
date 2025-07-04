using System;

namespace AsistenciaApi.Models
{
    public class RegistroAsistencia
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraEntradaComida { get; set; }
        public TimeSpan HoraSalidaComida { get; set; }
        public bool PermisoEntradaTarde { get; set; }
        public bool PermisoSalidaTemprana { get; set; }
    }



}
