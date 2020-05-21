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
    public class ClientesQuerys
    {
        public List<Clientes> GetClientes()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Clientes> listado = new List<Clientes>();
            listado = con.Query<Clientes>("SELECT Codigo,RazonSocial,FechaCreacion,IdUsuario FROM Clientes ORDER BY RazonSocial ASC").ToList();

            return listado;
        }

        public bool ExisteCliente(string cliente) //comprueba si existe la razon social devolviendo la cantidad de veces que existe
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            var existe = con.Query<int>("SELECT COUNT(RazonSocial) FROM Clientes WHERE RazonSocial='" + cliente + "'");
            if (existe.First() > 0) //si la cantidad es mayor a 0, significa que si existe
            {
                return true; //si existe, devuelve true
            }
            else
            {
                return false; //si no existe, devuelve false
            }
        }

        public bool InsertCliente(Clientes cliente)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int nuevo = con.Execute("INSERT INTO Clientes(RazonSocial,FechaCreacion,IdUsuario) VALUES(@RazonSocial,@FechaCreacion,@IdUsuario)",
                new { RazonSocial = cliente.RazonSocial, FechaCreacion = DateTime.Now, IdUsuario = cliente.IdUsuario });
            if (nuevo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Clientes> ClientePorCodigo(int codigo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Clientes> datosCliente = new List<Clientes>();
            datosCliente = con.Query<Clientes>("SELECT Codigo,RazonSocial,FechaCreacion,IdUsuario FROM Clientes WHERE Codigo=@Codigo",
                new { Codigo = codigo }).ToList();
            return datosCliente;
        }

        public bool DeleteCliente(int codigo) //elimina un cliente de acuerdo al numero de codigo
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int baja = con.Execute("DELETE FROM Clientes WHERE Codigo=@Codigo",
                new { Codigo = codigo });
            if (baja > 0) //si la cantidad es mayor a 0, significa que se eliminó
            {
                return true; //si se eliminó, devuelve true
            }
            else
            {
                return false; //si no se pudo eliminar, devuelve false
            }
        }

        public bool UpdateCliente(int codigo, string razonSocial, int idUsuario)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int edita = con.Execute("UPDATE Clientes SET RazonSocial=@RazonSocial, IdUsuario=@IdUsuario WHERE Codigo=@Codigo",
                new { Codigo = codigo, RazonSocial = razonSocial, IdUsuario = idUsuario });
            if (edita > 0) //si la cantidad es mayor a 0, significa que se modificó
            {
                return true; //si se modificó, devuelve true
            }
            else
            {
                return false; //si no se pudo modificar, devuelve false
            }
        }

        public List<Clientes> ClientePorNombre(string nombre)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Clientes> datosCliente = new List<Clientes>();
            datosCliente = con.Query<Clientes>("SELECT Codigo,RazonSocial,FechaCreacion,IdUsuario FROM Clientes WHERE RazonSocial LIKE '%" + nombre + "%'").ToList();
            return datosCliente;
        }
    }
}