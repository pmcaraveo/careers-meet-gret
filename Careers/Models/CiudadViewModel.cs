using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models
{
    public class CiudadViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }
        public SelectList Estado { get; set; }

        public CiudadViewModel ToViewModel (Ciudad ciudad)
        {
            var model = new CiudadViewModel()
            {
                Id = ciudad.Id,
                Nombre = ciudad.Nombre,
                EstadoId = ciudad.EstadoId,
                Activo = ciudad.Activo
            };
            return model;
        }

        public Ciudad ToCiudad()
        {
            var ciudad = new Ciudad()
            {
                Id = Id,
                Nombre = Nombre,
                EstadoId = EstadoId,
                Activo = Activo
            };
            return ciudad;
        }
    }
}