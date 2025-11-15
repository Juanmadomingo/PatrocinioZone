using System.Collections.Generic;

namespace PatrocinioZoneProyecto.Models
{
    public class Patrocinador : Usuario
    {
        public string Empresa { get; set; }
        public string Telefono { get; set; }

        // 👇 Agregá esta propiedad
        public List<Club> ClubsPatrocinados { get; set; } = new List<Club>();
    }
}