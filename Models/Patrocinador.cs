namespace PatrocinioZoneProyecto.Models
{
    public class Patrocinador : Usuario
    {
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Usamos decimal para representar dinero de manera segura
        public decimal Monto { get; set; } = 0m;

        // Inicializamos la lista para evitar nulls
        public List<ZonaPatrocinio> Zonas { get; set; } = new List<ZonaPatrocinio>();
    }
}





