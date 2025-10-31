using Sistema.Datos;
using System;
using System.Data;


namespace Sistema.Negocio
{

    public class NGrado
    {
        public static void Agregar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del grado no puede estar vacío.");

            DGrado.Insertar(nombre);
        }

        public static DataTable Mostrar()
        {
            return DGrado.Listar();
        }

        public static void Borrar(int id)
        {
            DGrado.Eliminar(id);
        }
    }
}