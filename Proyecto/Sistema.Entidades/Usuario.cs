namespace Sistema.Entidades
{
    // Clase que representa un usuario en el sistema
    public class Usuario
    {
        // Propiedad que almacena el ID del usuario (clave primaria en la base de datos)
        public int id_usuario { get; set; }

        // Propiedad que almacena el nombre de usuario
        public string nombre_usuario { get; set; }

        // Propiedad que almacena la contraseña del usuario
        public string contrasena { get; set; }

        // Propiedad que almacena el ID del rol asignado al usuario
        public int id_rol { get; set; }

        // Constructor vacío que permite crear un objeto Usuario sin inicializar propiedades
        public Usuario() { }

        // Constructor que inicializa todas las propiedades del usuario
        public Usuario(int id, string nombre, string contra, int idRol)
        {
            id_usuario = id;           // Asigna el ID del usuario
            nombre_usuario = nombre;   // Asigna el nombre de usuario
            contrasena = contra;       // Asigna la contraseña
            id_rol = idRol;            // Asigna el ID del rol
        }
    }
}
