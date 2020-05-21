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
    public class PedidosQuerys
    {
        public List<Pedidos> UltimoPedido()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Pedidos> pedidos = new List<Pedidos>();
            pedidos = con.Query<Pedidos>("SELECT TOP 1 NumeroPedido FROM Pedidos ORDER BY NumeroPedido DESC").ToList();
            return pedidos;
        }

    }
}