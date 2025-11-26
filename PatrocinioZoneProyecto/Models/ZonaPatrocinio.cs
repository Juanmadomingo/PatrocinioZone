using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Models
{
    public class ZonaPatrocinio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        // Relación con Club (cada zona pertenece a un club)
        public int ClubId { get; set; }
        public Club Club { get; set; } = null!;

        // Relación con Patrocinador (si ya fue comprada)
        public int? PatrocinadorId { get; set; }
        public Patrocinador? Patrocinador { get; set; }
    }
}
