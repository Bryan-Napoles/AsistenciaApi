namespace AsistenciaApi.DTOs
{
    public class RegistroAsistenciaDTO
    {
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
