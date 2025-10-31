using Sistema.Datos;
using System;
using System.Data;


namespace Sistema.Negocio
{

    public class NAsignacion
    {
        public static void Asignar(int idDocente, int idAsignatura, int idGrado)
            => DAsignacion.Insertar(idDocente, idAsignatura, idGrado);

        public static DataTable Mostrar() => DAsignacion.Listar();
    }
}