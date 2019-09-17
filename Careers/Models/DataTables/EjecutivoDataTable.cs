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
    public class EjecutivoDataTable
    {
        [DataTables(Visible = false)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Empresa { get; set; }

        public string NivelOrganizacional { get; set; }

        public string Descripcion { get; set; }

        [DataTables(DisplayName = "Colaborador")]
        public string Apoyo { get; set; }

        [DataTables(DisplayName = "", Searchable = false, Sortable = false, Width = "130px")]
        public string Tools { get; set; }
    }
}