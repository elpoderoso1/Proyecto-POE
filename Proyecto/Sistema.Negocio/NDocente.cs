using Sistema.Datos;
using System;
using System.Data;


namespace Sistema.Negocio
{

    public class NDocente
    {
        public static void Agregar(string nombre, string especialidad, string documento, string contacto, byte[] foto)
            => DDocente.Insertar(nombre, especialidad, documento, contacto, foto);

        public static DataTable Mostrar() => DDocente.Listar();

        public static void Borrar(int id) => DDocente.Eliminar(id);
        
        public static void Editar(int id, string nombre, string especialidad, string documento, string contacto, byte[] foto)
            => DDocente.Actualizar(id, nombre, especialidad, documento, contacto, foto);
    }
}
    