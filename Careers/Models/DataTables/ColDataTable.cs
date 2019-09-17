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
    public class ColDataTable
    {        
        [DataTables(Visible = false)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Empresa { get; set; }

        [DataTables(DisplayName = "Área")]
        public string Area { get; set; }
        

        [DataTables(DisplayName = "", Searchable = false, Sortable = false, Width = "130px")]
        public string Tools { get; set; }
    }
}