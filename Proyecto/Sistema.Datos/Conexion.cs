using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class Conexion
    {
        private static string cadena = "Server=DESKTOP-A1F1MEK\\SQLEXPRESS;Database=EscuelaDB;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}

