using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    // Clase para manejar todas las operaciones sobre la tabla Estudiantes
    public class DEstudiante
    {
        // Método para listar todos los estudiantes
        public DataTable Listar()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Estudiantes ORDER BY nombre", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    tabla.Load(dr);
                }
            }
            return tabla;
        }

        // Método para buscar estudiantes según un criterio en nombre, documento o correo
        public DataTable Buscar(string criterio)
        {
            DataTable tabla = new DataTable();
            string sql = @"SELECT * FROM Estudiantes 
                            WHERE nombre LIKE @criterio OR 
                                    documento LIKE @criterio OR 
                                    correo LIKE @criterio
                            ORDER BY nombre";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@criterio", $"%{criterio}%");
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    tabla.Load(dr);
                }
            }
            return tabla;
        }

        // Método para insertar un nuevo estudiante
        public string Insertar(string nombre, string documento, DateTime fecha_nacimiento,
                                string direccion, string telefono, string correo, string fotografia)
        {
            if (DocumentoExiste(documento))
                return "El documento ya está registrado";

            if (CorreoExiste(correo))
                return "El correo ya está registrado";

            string sql = @"INSERT INTO Estudiantes 
                            (nombre, documento, fecha_nacimiento, direccion, telefono, correo, fotografia)
                            VALUES (@nombre, @documento, @fecha_nac, @direccion, @telefono, @correo, @foto)";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@documento", documento);
                cmd.Parameters.AddWithValue("@fecha_nac", fecha_nacimiento);
                cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(direccion) ? DBNull.Value : (object)direccion);
                cmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(telefono) ? DBNull.Value : (object)telefono);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@foto", string.IsNullOrEmpty(fotografia) ? DBNull.Value : (object)fotografia);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0 ? "OK" : "No se pudo insertar";
            }
        }

        // Método para actualizar un estudiante existente
        public string Actualizar(int id_estudiante, string nombre, string documento, DateTime fecha_nacimiento,
                                string direccion, string telefono, string correo, string fotografia)
        {
            if (DocumentoExiste(documento, id_estudiante))
                return "El documento ya está registrado por otro estudiante";

            if (CorreoExiste(correo, id_estudiante))
                return "El correo ya está registrado por otro estudiante";

            string sql = @"UPDATE Estudiantes 
                            SET nombre = @nombre, documento = @documento, fecha_nacimiento = @fecha_nac,
                                direccion = @direccion, telefono = @telefono, correo = @correo, fotografia = @foto
                            WHERE id_estudiante = @id";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id_estudiante);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@documento", documento);
                cmd.Parameters.AddWithValue("@fecha_nac", fecha_nacimiento);
                cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(direccion) ? DBNull.Value : (object)direccion);
                cmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(telefono) ? DBNull.Value : (object)telefono);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@foto", string.IsNullOrEmpty(fotografia) ? DBNull.Value : (object)fotografia);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0 ? "OK" : "No se pudo actualizar";
            }
        }

        // Método para eliminar un estudiante
        public string Eliminar(int id_estudiante)
        {
            if (TieneMatriculasActivas(id_estudiante))
                return "No se puede eliminar, el estudiante tiene matrículas activas";

            string sql = "DELETE FROM Estudiantes WHERE id_estudiante = @id";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id_estudiante);
                cn.Open();
                return cmd.ExecuteNonQuery() > 0 ? "OK" : "No se pudo eliminar";
            }
        }

        // Método privado para verificar si un documento ya existe
        private bool DocumentoExiste(string documento, int idExcluir = 0)
        {
            string sql = idExcluir == 0 ?
                "SELECT COUNT(*) FROM Estudiantes WHERE documento = @documento" :
                "SELECT COUNT(*) FROM Estudiantes WHERE documento = @documento AND id_estudiante != @id";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@documento", documento);
                if (idExcluir != 0)
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Método privado para verificar si un correo ya existe
        private bool CorreoExiste(string correo, int idExcluir = 0)
        {
            string sql = idExcluir == 0 ?
                "SELECT COUNT(*) FROM Estudiantes WHERE correo = @correo" :
                "SELECT COUNT(*) FROM Estudiantes WHERE correo = @correo AND id_estudiante != @id";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@correo", correo);
                if (idExcluir != 0)
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Método privado para verificar si un estudiante tiene matrículas activas
        private bool TieneMatriculasActivas(int id_estudiante)
        {
            string sql = @"SELECT COUNT(*) FROM Matriculas 
                            WHERE estudiante = @id_estudiante";

            using (SqlConnection cn = Conexion.GetInstancia().CrearConexion())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id_estudiante", id_estudiante);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}
