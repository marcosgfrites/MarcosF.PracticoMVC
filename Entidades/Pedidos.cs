using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Pedidos
    {
        public int NumeroPedido { get; set; }
        public int CodigoCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
}