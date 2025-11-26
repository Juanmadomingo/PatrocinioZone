using System.Collections.Generic;

namespace PatrocinioZoneProyecto.Models
{
    public class Club : Usuario
    {
        public string Direccion { get; set; } = string.Empty;
        public Deporte Deporte { get; set; }
        public decimal MontoBase { get; set; }
        public List<ZonaPatrocinio> Zonas { get; set; } = new();

        public Club() : base() { }
    }
}
