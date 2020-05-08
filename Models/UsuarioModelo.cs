using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services.Protocols;

namespace PracticoMVC.Models
{
    public class UsuarioModelo
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(5, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Código de Rol")]
        public string IdRol { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(70, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(70, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(16, MinimumLength = 8,ErrorMessage = "El campo {0} tiene un mínimo permitido de {2} caracteres y un máximo de {1} caracteres")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "El campo {0} tiene un mínimo permitido de {2} caracteres y un máximo de {1} caracteres")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirmación de Password")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Activo { get; set; }
    }
}