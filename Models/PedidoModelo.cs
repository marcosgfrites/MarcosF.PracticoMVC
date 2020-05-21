using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticoMVC.Models
{
    public class PedidoModelo
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Cód. Cliente")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Razón Social")]
        public string RazonSocialCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Pedido Nro.")]
        public int NumeroPedido { get; set; }

    }

    public class PedidoBuscaClienteModelo
    {
        [Required]
        public string Cliente { get; set; }
    }
}