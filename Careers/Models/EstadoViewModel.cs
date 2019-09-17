using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models
{
    public class EstadoViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        [Display(Name = "País")]
        public int PaisId { get; set; }
        public SelectList Pais { get; set; }

        public EstadoViewModel ToViewModel (Estado estado)
        {
            var model = new EstadoViewModel()
            {
                Id=estado.Id,
                Nombre = estado.Nombre,
                PaisId = estado.PaisId,
                Activo = estado.Activo
            };
            return model;
        }

        public Estado ToEstado()
        {
            var estado = new Estado()
            {
                Id = Id,
                Nombre = Nombre,
                PaisId = PaisId,
                Activo = Activo
            };
            return estado;
        }
    }
}