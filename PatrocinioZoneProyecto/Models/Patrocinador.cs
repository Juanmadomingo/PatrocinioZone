using System.Collections.Generic;

namespace PatrocinioZoneProyecto.Models
{
    public class Patrocinador : Usuario
    {
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Presupuesto del patrocinador
        public decimal MontoDisponible { get; set; }

        // Zonas que tiene actualmente patrocinadas
        public List<ZonaPatrocinio> ZonasPatrocinadas { get; set; } = new List<ZonaPatrocinio>();

        public Patrocinador() : base() { }
    }
}