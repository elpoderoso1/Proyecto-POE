using Sistema.Datos;
using System.Data;
using Sistema.Entidades;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public static class DAsignatura
    {
        public static void Insertar(string codigo, string nombre, string descripcion, int creditos)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                string query = @"INSERT INTO Asignatura (Codigo, Nombre, Descripcion, Creditos)
                                 VALUES (@c, @n, @d, @cr)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@c", codigo);
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@d", descripcion);
                cmd.Parameters.AddWithValue("@cr", creditos);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable Listar()
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Asignatura", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Eliminar(int idAsignatura)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                string query = "DELETE FROM Asignatura WHERE IdAsignatura = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", idAsignatura);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void Actualizar(int id, string codigo, string nombre, string descripcion, int creditos)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                string query = @"UPDATE Asignatura 
                         SET Codigo = @c, Nombre = @n, Descripcion = @d, Creditos = @cr
                         WHERE IdAsignatura = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@c", codigo);
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@d", descripcion);
                cmd.Parameters.AddWithValue("@cr", creditos);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
