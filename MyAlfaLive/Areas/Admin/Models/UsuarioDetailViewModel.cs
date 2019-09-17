using MyAlfaLive.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAlfaLive.Areas.Admin.Models
{
    public class UsuarioDetailViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        public AspNetUsers Usuario { get; set; }

        [Display(Name = "Roles")]
        public List<AspNetRoles> Roles { get; set; }

    }
}