using System.Web;
using System.Web.Optimization;

namespace PracticoMVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/font-face.css",
                "~/Content/vendor/font-awesome-4.7/css/font-awesome.min.css",
                "~/Content/vendor/font-awesome-5/css/fontawesome-all.min.css",
                "~/Content/vendor/mdi-font/css/material-design-iconic-font.min.css",
                "~/Content/vendor/bootstrap-4.1/bootstrap.min.css",
                "~/Content/vendor/animsition/animsition.min.css",
                "~/Content/vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css",
                "~/Content/vendor/wow/animate.css",
                "~/Content/vendor/css-hamburgers/hamburgers.min.css",
                "~/Content/vendor/slick/slick.css",
                "~/Content/vendor/select2/select2.min.css",
                "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.css",
                "~/Content/css/theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Content/vendor/jquery-3.2.1.min.js",
                "~/Content/vendor/bootstrap-4.1/popper.min.js",
                "~/Content/vendor/bootstrap-4.1/bootstrap.min.js",
                "~/Content/vendor/slick/slick.min.js",
                "~/Content/vendor/wow/wow.min.js",
                "~/Content/vendor/animsition/animsition.min.js",
                "~/Content/vendor/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/Content/vendor/counter-up/jquery.waypoints.min.js",
                "~/Content/vendor/counter-up/jquery.counterup.min.js",
                "~/Content/vendor/circle-progress/circle-progress.min.js",
                "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.js",
                "~/Content/vendor/chartjs/Chart.bundle.min.js",
                "~/Content/vendor/select2/select2.min.js",
                "~/Content/js/main.js"));
        }
    }
}
