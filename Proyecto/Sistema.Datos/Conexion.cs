using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class Conexion
    {
        private string DB;
        private string SV;
        private static Conexion CON = null;
        private Conexion()
        {
            this.DB = "SistemaEscolar";
            this.SV = "localhost";
        }
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = $"Server={this.SV};Database={this.DB};Integrated Security=SSPI;";
            }
            catch (SqlException ex)
            {
                Cadena = null;
                throw ex;
                throw new System.Exception(ex.Message);
            }
            return Cadena;
        }
        public static Conexion getInstancia()
        {
            if (CON == null)
            {
                CON = new Conexion();
            }
            return CON;
        }
    }
}
