namespace PatrocinioZoneProyecto.Models
{
    public class Club : Usuario
    {
        public string Direccion { get; set; } = string.Empty;
        public string Deporte { get; set; } = string.Empty;
        public List<ZonaPatrocinio>? ZonasDePatrocinio { get; set; }
    }

}




