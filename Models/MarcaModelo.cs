using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace PracticoMVC.Models
{
    public class MarcaModelo
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50,ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
    }
}