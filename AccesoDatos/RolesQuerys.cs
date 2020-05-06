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
    public class RolesQuerys
    {
        public List<Roles> GetRoles()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Roles> listado = new List<Roles>();
            listado = con.Query<Roles>("SELECT Id,Descripcion FROM Roles ORDER BY Descripcion ASC").ToList();
            return listado;
        }

        public bool InsertRol(Roles rol)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int nuevo = con.Execute("INSERT INTO Roles(Id,Descripcion) VALUES(@Id,@Descripcion)",
                new { Id = rol.Id, Descripcion = rol.Descripcion });
            if (nuevo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteNombre(string codigo, string descripcion) //comprueba si existe la marca devolviendo la cantidad de veces que existe
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            var existe = con.Query<int>("SELECT COUNT(Descripcion) FROM Roles WHERE Descripcion='" + descripcion + "' OR Id='" + codigo + "'");
            if (existe.First() > 0) //si la cantidad es mayor a 0, significa que si existe
            {
                return true; //si existe, devuelve true
            }
            else
            {
                return false; //si no existe, devuelve false
            }
        }

        public List<Roles> RolPorCodigo(string id) //busco los datos de un rol, segun el valor de Id
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Roles> datosRol = new List<Roles>();
            datosRol = con.Query<Roles>("SELECT Id,Descripcion FROM Roles WHERE Id=@Id",
                new { Id = id }).ToList();
            return datosRol;
        }

        public bool DeleteRol(string idMarca) //elimina un rol de acuerdo al valor de Id
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int baja = con.Execute("DELETE FROM Roles WHERE Id=@Id",
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

        public bool UpdateRol(string idMarca, string nombreNuevo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int edita = con.Execute("UPDATE Roles SET Descripcion=@Descripcion WHERE Id=@Id",
                new { Id = idMarca, Descripcion = nombreNuevo });
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