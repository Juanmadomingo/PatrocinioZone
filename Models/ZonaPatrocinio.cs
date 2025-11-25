namespace PatrocinioZoneProyecto.Models
{
    public class ZonaPatrocinio
    {
        public int Id { get; set; }

        public Ubicacion Ubicacion { get; set; }
        public int Tamanio { get; set; }
        public bool EstadoReservado { get; set; }

        // FK obligatoria (toda zona pertenece a un club)
        public int ClubId { get; set; }
        public Club Club { get; set; }

        // FK opcional (puede o no tener patrocinador)
        public int? PatrocinadorId { get; set; }
        public Patrocinador? Patrocinador { get; set; }
    }
}

