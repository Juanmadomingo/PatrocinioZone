using System.ComponentModel.DataAnnotations;

namespace PatrocinioZoneProyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public Usuario() { }

        public Usuario(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }
    }
}
