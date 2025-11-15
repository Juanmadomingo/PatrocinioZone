namespace PatrocinioZoneProyecto.Models
{
    public class ZonaPatrocinio
    {
        public int Id { get; set; } // ← esto faltaba
        public double Tamanio { get; set; }
        public bool EstadoReservado { get; set; }
        public Ubicacion Ubicacion { get; set; }
    }
}
