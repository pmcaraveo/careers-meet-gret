using Careers.Domain;
using Mvc.JQuery.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models
{
    public class UsuarioViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Apellido { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public SelectList Empresas { get; set; }

        public UsuarioViewModel ToViewModel(AspNetUsers usuario)
        {
            var model = new UsuarioViewModel()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                EmpresaId = usuario.EmpresaId
            };
            return model;
        }

        public AspNetUsers ToUsuario()
        {
            var usuario = new AspNetUsers()
            {
                Id = Id,
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                EmpresaId = EmpresaId
            };
            return usuario;
        }
    }
}