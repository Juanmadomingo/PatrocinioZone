using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatrocinioZoneProyecto.Models
{
    public class Patrocinador : Usuario
    {
        [Required]
        public string Empresa { get; set; } = string.Empty;

        [Required, Phone]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto disponible debe ser positivo")]
        public decimal MontoDisponible { get; set; }

        public List<ZonaPatrocinio> ZonasPatrocinadas { get; set; } = new List<ZonaPatrocinio>();

        public Patrocinador() : base() { }
    }
}
