namespace PatrocinioZoneProyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // 🔥 Nuevo campo
        public string Password { get; set; } = string.Empty;

        public Usuario() { }

        public Usuario(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }
    }
}
