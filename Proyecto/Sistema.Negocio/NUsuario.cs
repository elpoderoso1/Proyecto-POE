using System;
using Sistema.Datos;
using Sistema.Entidades;
using System.Collections.Generic;

namespace Sistema.Negocio
{
    // Clase que representa la capa de negocio para los usuarios
    // Se encarga de aplicar validaciones antes de enviar datos a la capa de datos
    public class UsuarioNegocio
    {
        // Instancia de la clase UsuarioDatos para realizar operaciones CRUD en la base de datos
        private UsuarioDatos datos = new UsuarioDatos();

        // Método para agregar un nuevo usuario
        public void AgregarUsuario(Usuario u)
        {
            // Validación: el nombre de usuario no puede estar vacío
            if (string.IsNullOrEmpty(u.nombre_usuario))
                throw new Exception("El nombre de usuario no puede estar vacío.");

            // Validación: la contraseña no puede estar vacía
            if (string.IsNullOrEmpty(u.contrasena))
                throw new Exception("La contraseña no puede estar vacía.");

            // Llama a la capa de datos para insertar el usuario
            datos.Insertar(u);
        }

        // Método para listar todos los usuarios
        public List<Usuario> ListarUsuarios()
        {
            // Llama a la capa de datos para obtener todos los usuarios
            return datos.ObtenerTodos();
        }

        // Método para editar un usuario existente
        public void EditarUsuario(Usuario u)
        {
            // Validación: el ID del usuario debe ser válido
            if (u.id_usuario <= 0)
                throw new Exception("El usuario no es válido.");

            // Llama a la capa de datos para actualizar el usuario
            datos.Actualizar(u);
        }

        // Método para eliminar un usuario
        public void EliminarUsuario(int id)
        {
            // Validación: el ID del usuario debe ser válido
            if (id <= 0)
                throw new Exception("El ID de usuario no es válido.");

            // Llama a la capa de datos para eliminar el usuario
            datos.Eliminar(id);
        }
    }
}
