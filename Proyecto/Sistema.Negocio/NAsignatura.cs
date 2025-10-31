using Sistema.Datos;
using System;
using System.Data;


namespace Sistema.Negocio
{
    public class NAsignatura
    {
        public static void Agregar(string codigo, string nombre, string descripcion, int creditos)
            => DAsignatura.Insertar(codigo, nombre, descripcion, creditos);

        public static DataTable Mostrar() => DAsignatura.Listar();

        public static void Eliminar(int id) => DAsignatura.Eliminar(id);

        public static void Editar(int id, string codigo, string nombre, string descripcion, int creditos)
             => DAsignatura.Actualizar(id, codigo, nombre, descripcion, creditos);

    }
}