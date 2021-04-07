using System.Web.Optimization;

namespace StoreFrontLab.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
           
            bundles.Add(new ScriptBundle("~/Content/scripts").Include(
                      "~/Content/js/jquery-3.3.1.min.js",
                      "~/Content/js/bootstrap.min.js",
                      "~/Content/js/jquery.nice-select.min.js",
                      "~/Content/js/jquery-ui.min.js",
                      "~/Content/js/jquery.slicknav.js",
                      "~/Content/js/mixitup.min.js",
                      "~/Content/js/owl.carousel.min.js",
                      "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/elegant-icons.css",
                      "~/Content/css/nice-select.css",
                      "~/Content/css/jquery-ui.min.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/slicknav.min.css"));
        }
    }
}
