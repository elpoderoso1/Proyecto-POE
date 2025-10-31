using Sistema.Datos;
using System.Data;
using System.Data.SqlClient;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public static class DAsignacion
    {
        public static void Insertar(int idDocente, int idAsignatura, int idGrado)
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                string query = @"INSERT INTO AsignacionDocente (IdDocente, IdAsignatura, IdGrado)
                                 VALUES (@d, @a, @g)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@d", idDocente);
                cmd.Parameters.AddWithValue("@a", idAsignatura);
                cmd.Parameters.AddWithValue("@g", idGrado);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable Listar()
        {
            using (SqlConnection con = Conexion.GetInstancia().CrearConexion())
            {
                string query = @"SELECT ad.IdAsignacion, d.Nombre AS Docente, a.Nombre AS Asignatura, g.Nombre AS Grado
                                 FROM AsignacionDocente ad
                                 JOIN Docente d ON ad.IdDocente = d.IdDocente
                                 JOIN Asignatura a ON ad.IdAsignatura = a.IdAsignatura
                                 JOIN Grado g ON ad.IdGrado = g.IdGrado";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
