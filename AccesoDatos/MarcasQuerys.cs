using Dapper;
using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class MarcasQuerys
    {
        public List<Marcas> GetMarcas() //devuelve un listado general de las marcas existentes
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Marcas> listado = new List<Marcas>();
            listado = con.Query<Marcas>("SELECT Id,Nombre FROM Marcas ORDER BY Nombre ASC").ToList();
            return listado;
        }

        public bool InsertMarca(Marcas marca) //inserta una marca nueva
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int nuevo = con.Execute("INSERT INTO Marcas(Nombre) VALUES(@Nombre)",
                new { Nombre = marca.Nombre });
            if (nuevo > 0) //si el resultado de la inserción es exitosa, el contador "nuevo" será distinto a 0
            {
                return true; //si es distinto a 0, devuelve true
            }
            else
            {
                return false; //si es 0 o menor, devuelve false;
            }
        }

        public bool ExisteNombre (string nombre) //comprueba si existe la marca devolviendo la cantidad de veces que existe
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            var existe = con.Query<int>("SELECT COUNT(Nombre) FROM Marcas WHERE Nombre='" + nombre + "'");
            if (existe.First() > 0) //si la cantidad es mayor a 0, significa que si existe
            {
                return true; //si existe, devuelve true
            }
            else
            {
                return false; //si no existe, devuelve false
            }
        }

        public bool DeleteMarca(int idMarca) //elimina una marca de acuerdo al numero de Id
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int baja = con.Execute("DELETE FROM Marcas WHERE Id=@Id",
                new { Id = idMarca });
            if (baja > 0) //si la cantidad es mayor a 0, significa que se eliminó
            {
                return true; //si se eliminó, devuelve true
            }
            else
            {
                return false; //si no se pudo eliminar, devuelve false
            }
        }

        public List<Marcas> MarcaPorCodigo(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Marcas> datosMarca = new List<Marcas>();
            datosMarca = con.Query<Marcas>("SELECT Id,Nombre FROM Marcas WHERE Id=@Id",
                new { Id = id }).ToList();
            return datosMarca;
        }

        public bool UpdateMarca(int idMarca, string nombreNuevo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int edita = con.Execute("UPDATE Marcas SET Nombre=@Nombre WHERE Id=@Id",
                new { Id = idMarca, Nombre = nombreNuevo});
            if (edita > 0) //si la cantidad es mayor a 0, significa que se eliminó
            {
                return true; //si se eliminó, devuelve true
            }
            else
            {
                return false; //si no se pudo eliminar, devuelve false
            }
        }
    }
}