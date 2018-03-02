using System.Web;
using System.Web.Optimization;

namespace RecruitmentSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
          
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.*", "~/Scripts/jquery.validate.unobtrusive.*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-confirm").Include("~/Scripts/jquery-confirm.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", "~/Content/jquery-confirm.css"));
            bundles.Add(new StyleBundle("~/Content/sb-admin").Include("~/Content/sb-admin.css"));
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                        "~/Scripts/moment.min.js", "~/Scripts/bootstrap-datetimepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/custom").Include("~/Scripts/custom-script.js"));
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include("~/Content/bootstrap-datetimepicker.css"));
        }
    }
}
