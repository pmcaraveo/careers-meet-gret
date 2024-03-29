﻿using Careers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models
{
    public class AreaViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "El campo {0} no puede exceder de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}