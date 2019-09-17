using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Careers.Helpers
{
    public static class DataTablesButtons
    {
        public static string GetEditButton (string url)
        {
            var button = "<a class= \"btn btn-primary\" href = \"" + url + " \" >" + "<i class= \"fas fa-pencil-alt\"></i>" + "</a>";
            return button;
        }

        public static string GetDeleteButton (string url)
        {
            var button = "<a class= \"btn btn-primary\" href = \"" + url + " \" >" + "<i class= \"fas fa-times\"></i>" + "</a>";
            return button;
        }

        public static string GetDetailsButton (string url)
        {
            var button = "<a class= \"btn btn-primary\" href = \"" + url + " \" >" + "<i class= \"fas fa-search-plus\"></i>" + "</a>";
            return button;
        }
    }
}