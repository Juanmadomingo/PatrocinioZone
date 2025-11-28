using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatrocinioZoneProyecto.Models
{
    public class Club : Usuario
    {
        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public Deporte Deporte { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
        public decimal MontoBase { get; set; }

        public List<ZonaPatrocinio> Zonas { get; set; } = new();

        public Club() : base() { }
    }
}
