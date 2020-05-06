using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticoMVC.Models
{
    public class UsuarioModelo
    {
        [Required]
        [DisplayName("Código")]
        private int Id { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Código de Rol")]
        private string IdRol { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Usuario")]
        private string Usuario { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Nombre")]
        private string Nombre { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Apellido")]
        private string Apellido { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "El campo {0} tiene un máximo permitido de {1} caracteres")]
        [DisplayName("Password")]
        private string Password { get; set; }

        [Required]
        private int Activo { get; set; }
    }
}