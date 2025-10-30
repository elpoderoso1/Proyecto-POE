using System;
using System.Data;
using Sistema.Datos;

namespace Sistema.Negocio
{
    public class NEstudiante
    {
        private static readonly DEstudiante datos = new DEstudiante();

        public static DataTable Listar()
            => datos.Listar();

        public static DataTable Buscar(string criterio)
            => datos.Buscar(criterio);

        public static string Insertar(string nombre, string documento, DateTime fecha_nacimiento,
                                    string direccion, string telefono, string correo, string fotografia = null)
        {
            if (DateTime.Now.Year - fecha_nacimiento.Year < 15)
                return "El estudiante debe tener al menos 15 años";

            if (string.IsNullOrWhiteSpace(documento))
                return "El documento es obligatorio";

            if (string.IsNullOrWhiteSpace(correo))
                return "El correo es obligatorio";

            return datos.Insertar(nombre, documento, fecha_nacimiento, direccion, telefono, correo, fotografia);
        }

        public static string Actualizar(int id_estudiante, string nombre, string documento, DateTime fecha_nacimiento,
                                        string direccion, string telefono, string correo, string fotografia = null)
        {
            if (DateTime.Now.Year - fecha_nacimiento.Year < 15)
                return "El estudiante debe tener al menos 15 años";

            return datos.Actualizar(id_estudiante, nombre, documento, fecha_nacimiento, direccion, telefono, correo, fotografia);
        }

        public static string Eliminar(int id_estudiante)
            => datos.Eliminar(id_estudiante);
    }
}