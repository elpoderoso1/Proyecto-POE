namespace Sistema.Entidades
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public int id_rol { get; set; }

        public Usuario() { }

        public Usuario(int id, string nombre, string contra, int idRol)
        {
            id_usuario = id;
            nombre_usuario = nombre;
            contrasena = contra;
            id_rol = idRol;
        }
    }
}