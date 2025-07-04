using System.ComponentModel.DataAnnotations;

namespace AsistenciaApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }  // Nullable
        public string? ApellidoMaterno { get; set; }  // Nullable

        [Required]
        public string Contrasena { get; set; }
    }

}
