using Careers.Domain;
using Mvc.JQuery.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Careers.Models.Display
{
    public class AreaDataTable
    {
        [DataTables(Visible = false)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }

        [DataTables(DisplayName = "", Searchable = false, Sortable = false, Width = "130px")]
        public string Tools { get; set; }
    }
}