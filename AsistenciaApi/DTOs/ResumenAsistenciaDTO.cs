namespace AsistenciaApi.DTOs
{
    public class ResumenAsistenciaDTO
    {
        public int TotalAsistencias { get; set; }
        public int TotalRetardos { get; set; }
        public int TotalSalidasTempranas { get; set; }
        public int TotalPermisosEspeciales { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}
