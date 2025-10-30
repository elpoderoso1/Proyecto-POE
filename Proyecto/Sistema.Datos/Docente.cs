using Sistema.Datos;
using System;
using Sistema.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{

    public static class Docente
    {
        public static void Insertar(string nombre, string especialidad, string documento, string contacto, byte[] foto)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Docente (Nombre, Especialidad, Documento, Contacto, Fotografia)
                                 VALUES (@n, @e, @d, @c, @f)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@e", especialidad);
                cmd.Parameters.AddWithValue("@d", documento);
                cmd.Parameters.AddWithValue("@c", contacto);
                cmd.Parameters.Add("@f", SqlDbType.VarBinary).Value = (object)foto ?? DBNull.Value;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable Listar()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Docente", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Docente WHERE IdDocente=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
