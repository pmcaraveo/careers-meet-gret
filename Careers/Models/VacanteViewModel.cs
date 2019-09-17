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

    public class VacanteViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string UrlDestino { get; set; }

        [Required]
        [StringLength(4000, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Área laboral")]
        public int AreaId { get; set; }
        public SelectList Areas { get; set; }

        [Required]
        [Display(Name = "País")]
        public int PaisId { get; set; }
        public SelectList Paises { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int CiudadId { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public SelectList Empresas { get; set; }

        [Required]
        [Display(Name = "Nivel Organizacional")]
        public int NivelOrganizacionalId { get; set; }
        public SelectList NivelOrganizacional { get; set; }

        [Required]
        [Display(Name = "Tipo de publicación")]
        public int TipoPublicacionId { get; set; }
        public SelectList TipoPublicacion { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Fecha de finalización")]
        public DateTime FechaFinalizacion { get; set; }

        public VacanteViewModel()
        {
            FechaInicio = DateTime.Now.Date;
            FechaFinalizacion = DateTime.Now.Date;
        } 

        public VacanteViewModel ToViewModel(Vacante vacante)
        {
            var model = new VacanteViewModel()
            {
                Id = vacante.Id,
                Nombre = vacante.Nombre,
                EmpresaId = vacante.EmpresaId,
                CiudadId = vacante.CiudadId,
                AreaId = vacante.AreaId,
                NivelOrganizacionalId = vacante.NivelOrganizacionalId,
                TipoPublicacionId = vacante.TipoPublicacionId,
                UrlDestino = vacante.UrlDestino,
                Descripcion = vacante.Descripcion,
                FechaInicio =  vacante.FechaInicio,
                FechaFinalizacion = vacante.FechaFinalizacion
            };
            return model;
        }

        public Vacante ToVacante()
        {
            var vacante = new Vacante()
            {
                Id = Id,
                Nombre = Nombre,
                EmpresaId = EmpresaId,
                CiudadId = CiudadId,
                AreaId = AreaId,
                NivelOrganizacionalId = NivelOrganizacionalId,
                TipoPublicacionId = TipoPublicacionId,
                UrlDestino = UrlDestino,
                Descripcion = Descripcion,
                FechaInicio = FechaInicio,
                FechaFinalizacion = FechaFinalizacion
            };
            return vacante;
        }
    }
}