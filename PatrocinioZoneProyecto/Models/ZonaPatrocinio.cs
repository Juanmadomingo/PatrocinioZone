using System.ComponentModel.DataAnnotations;

namespace PatrocinioZoneProyecto.Models
{
    public class ZonaPatrocinio
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser positivo")]
        public decimal Precio { get; set; }

        [Required]
        public Ubicacion Ubicacion { get; set; }

        // Relación con Club (cada zona pertenece a un club)
        public int ClubId { get; set; }
        public Club Club { get; set; } = null!;

        // Relación con Patrocinador (si ya fue comprada)
        public int? PatrocinadorId { get; set; }
        public Patrocinador? Patrocinador { get; set; }
    }
}
