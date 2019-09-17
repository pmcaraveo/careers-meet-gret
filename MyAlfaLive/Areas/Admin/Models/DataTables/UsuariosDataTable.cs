using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.JQuery.DataTables;

namespace MyAlfaLive.Areas.Admin.Models
{
    public class UsuariosDataTable
    {
        [DataTables(Visible = false)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        //public string Rol { get; set; }

        //public string Accesos { get; set; }

        [DataTables(DisplayName = "Herramientas", Searchable = false, Sortable = false, Width = "140px")]        
        public string Tools { get; set; }
    }
}