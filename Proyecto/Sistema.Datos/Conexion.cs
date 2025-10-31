using System;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    /// Clase para manejar la conexión a la base de datos usando Singleton.
    public class Conexion
    {
        private string DB; // Nombre de la base de datos
        private string SV; // Servidor de SQL
        private static Conexion instancia = null; // Instancia única de la clase

        /// Constructor privado para implementar Singleton
        private Conexion()
        {
            this.DB = "SistemaEscolar"; // Cambiar por tu DB
            this.SV = "localhost";      // Cambiar por tu servidor
        }

        /// Obtiene la instancia única de la clase Conexion
        public static Conexion GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Conexion();
            }
            return instancia;
        }

        /// Crea y devuelve un objeto SqlConnection listo para usar.
        public SqlConnection CrearConexion()
        {
            try
            {
                string cadenaConexion = $"Server={this.SV};Database={this.DB};Integrated Security=SSPI;";
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                return conexion;
            }
            catch (SqlException ex)
            {
                // Aquí puedes loguear el error o mostrar mensaje
                throw new Exception("Error al crear la conexión: " + ex.Message);
            }
        }
    }
}
