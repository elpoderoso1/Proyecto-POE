using System;
using System.Data;
using Sistema.Datos;

namespace Sistema.Negocio
{
    // Clase que representa la capa de negocio para Estudiantes
    // Aquí se aplican reglas de validación antes de enviar datos a la capa de datos
    public class NEstudiante
    {
        // Instancia de la clase DEstudiante de la capa de datos para acceder a la base de datos
        private static readonly DEstudiante datos = new DEstudiante();

        // Método que lista todos los estudiantes
        // Llama directamente al método Listar de la capa de datos
        public static DataTable Listar()
            => datos.Listar();

        // Método que busca estudiantes según un criterio (nombre, documento o correo)
        public static DataTable Buscar(string criterio)
            => datos.Buscar(criterio);

        // Método para insertar un nuevo estudiante aplicando validaciones
        public static string Insertar(string nombre, string documento, DateTime fecha_nacimiento,
                                    string direccion, string telefono, string correo, string fotografia = null)
        {
            // Validación: el estudiante debe tener al menos 15 años
            if (DateTime.Now.Year - fecha_nacimiento.Year < 15)
                return "El estudiante debe tener al menos 15 años";

            // Validación: el documento no puede estar vacío
            if (string.IsNullOrWhiteSpace(documento))
                return "El documento es obligatorio";

            // Validación: el correo no puede estar vacío
            if (string.IsNullOrWhiteSpace(correo))
                return "El correo es obligatorio";

            // Si pasa las validaciones, llama a la capa de datos para insertar el registro
            return datos.Insertar(nombre, documento, fecha_nacimiento, direccion, telefono, correo, fotografia);
        }

        // Método para actualizar un estudiante aplicando validaciones
        public static string Actualizar(int id_estudiante, string nombre, string documento, DateTime fecha_nacimiento,
                                        string direccion, string telefono, string correo, string fotografia = null)
        {
            // Validación: el estudiante debe tener al menos 15 años
            if (DateTime.Now.Year - fecha_nacimiento.Year < 15)
                return "El estudiante debe tener al menos 15 años";

            // Llama a la capa de datos para actualizar el registro
            return datos.Actualizar(id_estudiante, nombre, documento, fecha_nacimiento, direccion, telefono, correo, fotografia);
        }

        // Método para eliminar un estudiante
        // Llama directamente al método Eliminar de la capa de datos
        public static string Eliminar(int id_estudiante)
            => datos.Eliminar(id_estudiante);
    }
}
