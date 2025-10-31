using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sistema.Entidades;

namespace Sistema.Datos
{
    // Clase que maneja todas las operaciones de la tabla Usuarios
    public class UsuarioDatos
    {
        // Método para insertar un nuevo usuario en la base de datos
        public void Insertar(Usuario u)
        {
            // Usamos la conexión desde Conexion.cs
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                con.Open(); // Abrir conexión
                string sql = "INSERT INTO Usuarios (nombre_usuario, contrasena, id_rol) VALUES (@nombre, @contrasena, @idRol)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nombre", u.nombre_usuario);
                cmd.Parameters.AddWithValue("@contrasena", u.contrasena);
                cmd.Parameters.AddWithValue("@idRol", u.id_rol);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para obtener todos los usuarios
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Usuarios";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario(
                        Convert.ToInt32(dr["id_usuario"]),
                        dr["nombre_usuario"].ToString(),
                        dr["contrasena"].ToString(),
                        Convert.ToInt32(dr["id_rol"])
                    ));
                }
            }
            return lista;
        }

        // Método para actualizar los datos de un usuario existente
        public void Actualizar(Usuario u)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                con.Open();
                string sql = "UPDATE Usuarios SET nombre_usuario=@nombre, contrasena=@contrasena, id_rol=@idRol WHERE id_usuario=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nombre", u.nombre_usuario);
                cmd.Parameters.AddWithValue("@contrasena", u.contrasena);
                cmd.Parameters.AddWithValue("@idRol", u.id_rol);
                cmd.Parameters.AddWithValue("@id", u.id_usuario);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para eliminar un usuario de la base de datos
        public void Eliminar(int id)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                con.Open();
                string sql = "DELETE FROM Usuarios WHERE id_usuario=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
