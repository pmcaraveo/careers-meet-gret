using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Careers.Models
{
    public class ColViewModel
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public string Nombre { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Telefono { set; get; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { set; get; }
        public SelectList Empresa { set; get; }

        [Required]
        [Display(Name = "Area")]
        public int AreaId { set; get; }
        public SelectList Area { set; get; }

        public ColViewModel ToViewModel(Colaborador col)
        {
            var model = new ColViewModel()
            {
                Id = col.Id,
                Nombre = col.Nombre,
                Email = col.Email,
                Telefono = col.Telefono,
                EmpresaId = col.EmpresaId,
                AreaId = col.AreaId
            };
            return model;
        }

        public Colaborador ToCol()
        {
            var col = new Colaborador()
            {
                Id = Id,
                Nombre = Nombre,
                Email = Email,
                Telefono = Telefono,
                EmpresaId = EmpresaId,
                AreaId = AreaId
            };
            return col;
        }

    }
}