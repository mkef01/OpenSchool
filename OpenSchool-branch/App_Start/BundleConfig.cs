using System.Web;
using System.Web.Optimization;

namespace OpenSchool
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
				"~/Scripts/bootstrap/css/bootstrap.min.css", "~/Scripts/font-awesome/css/font-awesome.min.css", "~/Scripts/css/fontastic.css",
				 "~/Scripts/css/grasp_mobile_progress_circle-1.0.0.min.css",
				"~/Scripts/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css", "~/Scripts/css/style.default.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Content/Selectize/js/standalone/selectize.min.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Selectize/css/selectize.css",
                      "~/Content/Selectize/css/selectize.bootstrap3.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
