using System.Web;
using System.Web.Optimization;

namespace Ecommerce
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/slick.min.js",
                      "~/Scripts/nouislider.min.js",
                      "~/Scripts/jquery.zoom.min.js",
                      "~/Scripts/main.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //slick slider css
                      "~/Content/slick.css",
                       "~/Content/slick-theme.css",
                       //nouislider css
                       "~/Content/nouislider.min.css",
                       //font awesome
                       "~/Content/font-awesome.min.css",
                       "~/Content/all.min.css",
                      //custom stylesheet
                      "~/Content/site.css",
                      "~/Content/style.css"));
        }
    }
}
