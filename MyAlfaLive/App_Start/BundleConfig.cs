using System.Web;
using System.Web.Optimization;

namespace MyAlfaLive
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Datatables

            bundles.Add(new StyleBundle("~/Content/datatablescss").Include(
                 "~/Content/jquery-datatables-column-filter/media/css/themes/base/jquery-ui.css"
                , "~/Content/DataTables/css/dataTables.bootstrap4.css"
                , "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.css"
                , "~/Content/DataTables/css/reponsive.bootstrap4.css"
                , "~/Content/DataTables/css/buttons.dataTables.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                  "~/Scripts/jquery-ui-1.12.1.js"
                , "~/Scripts/DataTables/jquery.dataTables.js"
                , "~/Scripts/DataTables/dataTables.bootstrap4.js"
                , "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.js"
                , "~/Scripts/DataTables/dataTables.responsive.js"
                , "~/Scripts/DataTables/responsive.bootstrap4.js"
                , "~/Scripts/custom.dataTables.js"
                , "~/Scripts/DataTables/dataTables.buttons.js"
                , "~/Scripts/DataTables/buttons.html5.js"
                , "~/Scripts/DataTables/buttons.print.js"
                , "~/Scripts/DataTables/jszip.min.js"
                , "~/Scripts/DataTables/pdfmake.min.js"
                , "~/Scripts/DataTables/vfs_fonts.js"
            ));

            #endregion
        }
    }
}