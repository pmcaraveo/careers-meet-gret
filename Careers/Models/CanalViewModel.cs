using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Careers.Models
{
    public class CanalViewModel
    {
        [Required]
        public int Id { set; get; }

        [Required]
        
        public string Nombre { set; get; }


        [Required]
        public bool Activo { get; set; }

        //public CanalViewModel() { }

        //public CanalViewModel(Canal canal)
        //{
        //    Id = canal.Id;
        //    Nombre = canal.Nombre;
        //    Activo = canal.Activo;
        //}

        public Canal ToCanal(int usuarioId) {
            return new Canal
            {
                Nombre = Nombre.ToUpper(),
                UsuarioCreoId = usuarioId,
                FechaCreacion = DateTime.Now,
                Activo = Activo
            };
        }

        public Canal ToCanalUpdate(int usuarioId)
        {
            return new Canal
            {
                Nombre = Nombre.ToUpper(),
                UsuarioCreoId = usuarioId,
                FechaCreacion = DateTime.Now,
                Activo = Activo
            };
        }



    }
}