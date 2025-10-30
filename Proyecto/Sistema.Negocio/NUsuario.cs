using System;
using Sistema.Datos;
using Sistema.Entidades;
using System.Collections.Generic;

namespace Sistema.Negocio
{
    public class UsuarioNegocio
    {
        private UsuarioDatos datos = new UsuarioDatos();

        public void AgregarUsuario(Usuario u)
        {
            if (string.IsNullOrEmpty(u.nombre_usuario))
                throw new Exception("El nombre de usuario no puede estar vacío.");
            if (string.IsNullOrEmpty(u.contrasena))
                throw new Exception("La contraseña no puede estar vacía.");
            datos.Insertar(u);
        }

        public List<Usuario> ListarUsuarios()
        {
            return datos.ObtenerTodos();
        }

        public void EditarUsuario(Usuario u)
        {
            if (u.id_usuario <= 0)
                throw new Exception("El usuario no es válido.");
            datos.Actualizar(u);
        }

        public void EliminarUsuario(int id)
        {
            if (id <= 0)
                throw new Exception("El ID de usuario no es válido.");
            datos.Eliminar(id);
        }
    }
}
