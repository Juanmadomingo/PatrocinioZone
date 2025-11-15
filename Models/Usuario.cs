namespace PatrocinioZoneProyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Constructor vacío requerido por Entity Framework
        public Usuario() { }

        // Constructor con parámetros
        public Usuario(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }
    }
}