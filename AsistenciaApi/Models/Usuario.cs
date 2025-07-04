using System.ComponentModel.DataAnnotations;

namespace AsistenciaApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required] // Esto asegura que el nombre no sea nulo
        public string Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }  // Permite que sea nulo

        public string? ApellidoMaterno { get; set; }  // Permite que sea nulo

        [Required] // Esto asegura que la contraseña no sea nula
        public string Contrasena { get; set; }

        // Constructor opcional si necesitas inicializar valores por defecto
        public Usuario()
        {
            Nombre = string.Empty;  // Valor predeterminado vacío
            ApellidoPaterno = string.Empty; // Valor predeterminado vacío
            ApellidoMaterno = string.Empty; // Valor predeterminado vacío
            Contrasena = string.Empty; // Valor predeterminado vacío
        }
    }
}
