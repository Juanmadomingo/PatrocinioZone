namespace PatrocinioZoneProyecto.Models
{
    // Clase base NO mapeada (DbContext la ignora)
    public abstract class Usuario
    {
        public int Id { get; set; }   // PK heredada por Club y Patrocinador
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
