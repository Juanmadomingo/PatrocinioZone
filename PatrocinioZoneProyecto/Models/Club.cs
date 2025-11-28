using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatrocinioZoneProyecto.Models
{
    public class Club : Usuario
    {
        [Required, Phone]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public Deporte Deporte { get; set; }

        // No obligamos el monto en el registro (se acumula luego)
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
        public decimal MontoBase { get; set; } = 0m;

        public List<ZonaPatrocinio> Zonas { get; set; } = new();

        public Club() : base() { }
    }
}