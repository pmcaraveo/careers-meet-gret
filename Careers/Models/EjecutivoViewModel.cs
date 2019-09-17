using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models
{
    public class EjecutivoViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public SelectList Empresa { get; set; }

        [Required]
        [Display(Name = "Nivel")]
        public int NivelOrganizacionalId { get; set; }
        public SelectList NivelOrganizacional { get; set; }

        [Required]
        [Display(Name = "Canal")]
        public int CanalId { get; set; }
        public SelectList Canal { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Descripcion { get; set; }

       

        [Required]
        [Display(Name = "Colaborador")]
        public int ApoyoId { get; set; }
        public SelectList Apoyo { get; set; }
        public string Email { set; get; }
        public string Telefono { set; get; }

        public EjecutivoViewModel ToViewModel (Ejecutivos ejecutivo)
        {
            var model = new EjecutivoViewModel()
            {
                Id=ejecutivo.Id,
                Nombre = ejecutivo.Nombre,
                EmpresaId = ejecutivo.EmpresaId,
                NivelOrganizacionalId = ejecutivo.NivelOrganizacionalId,
                ApoyoId = ejecutivo.ApoyoId,
                Descripcion = ejecutivo.Descripcion,
                CanalId = ejecutivo.CanalId
            };
            return model;
        }

        public Ejecutivos ToEjecutivo()
        {
            var ejecutivo = new Ejecutivos()
            {
                Id = Id,
                Nombre = Nombre,
                EmpresaId = EmpresaId,
                NivelOrganizacionalId = NivelOrganizacionalId,
                ApoyoId = ApoyoId,
                Descripcion = Descripcion,
                CanalId =  CanalId
            };
            return ejecutivo;
        }
    }
}