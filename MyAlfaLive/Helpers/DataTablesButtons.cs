using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAlfaLive.Helpers
{
    public static class DataTablesButtons
    {
        public static string DefaultButtonCssClass { get; } = "btn btn-outline-primary btn-sm m-0 mr-1";

        public static string GetEditButton(string url)
        {
            return "<a class=\"" + DefaultButtonCssClass + "\" href=\"" + url + "\" title=\"Editar\"><i class=\"fas fa-pencil-alt\"></i></a>";
        }

        public static string GetDeleteButton (string url)
        {
            var button = "<a class= \""+ DefaultButtonCssClass+ "\" href = \"" + url + " \" >" + "<i class= \"fas fa-times\"></i>" + "</a>";
            return button;
        }

        public static string GetDetailsButton (string url)
        {
            var button = "<a class= \"" + DefaultButtonCssClass + "\" href = \"" + url + " \" >" + "<i class= \"fas fa-search-plus\"></i>" + "</a>";
            return button;
        }
    }
}