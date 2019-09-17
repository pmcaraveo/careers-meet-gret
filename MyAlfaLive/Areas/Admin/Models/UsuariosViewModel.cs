using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyAlfaLive.Domain;
using System.Web.Mvc;
using MyAlfaLive.Models;

namespace MyAlfaLive.Areas.Admin.Models
{
    public class UsuariosViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Apellido { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }

        public SelectList Empresas { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int[] RolesId { get; set; }

        public SelectList Roles { get; set; }

        //[Required]
        //[Display(Name = "Accesos")]
        //public string Accesos { get; set; }

        public bool Activo { get; set; }
    }
}