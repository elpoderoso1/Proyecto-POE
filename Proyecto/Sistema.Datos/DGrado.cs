using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class DGrado
    {
        public static DataTable Listar()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT IdGrado, Nombre FROM Grado", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }
            return tabla;
        }

        public static void Insertar(string nombre)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Grado (Nombre) VALUES (@nombre)", con);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int idGrado)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Grado WHERE IdGrado = @id", con);
                cmd.Parameters.AddWithValue("@id", idGrado);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}