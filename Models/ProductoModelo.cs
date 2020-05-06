using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PracticoMVC.Models
{
    public class ProductoModelo
    {
        private const int V = 0;
        private const int V1 = 1;

        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(255, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Marca")]
        public int IdMarca { get; set; }

        public string NombreMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Precio Unitario")]
        public decimal PrecioUnitario { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Estado")]
        public int Activo { get; set; }

        [DisplayName("URL de Imagen")]
        public string UrlImange { get; set; }
    }
}