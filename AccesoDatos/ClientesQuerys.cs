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

    }
}