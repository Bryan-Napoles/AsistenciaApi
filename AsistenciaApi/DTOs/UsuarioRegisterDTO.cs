namespace AsistenciaApi.DTOs
{
    public class UsuarioRegisterDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
