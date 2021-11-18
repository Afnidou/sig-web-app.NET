using System.Web;
using System.Web.Optimization;

namespace WebApplication3
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      ));
            #region Template Desing

            bundles.Add(new ScriptBundle("~/template/js").Include("~/js/jquery.js"
                      ,
                        "~/js/jquery.easing.1.3.js",
                        "~/js/bootstrap.min.js",
                        "~/js/jquery.fancybox.pack.js",
                        "~/js/jquery.fancybox-media.js",
                        "~/js/jquery.flexslider.js",
                        "~/js/animate.js",
                        "~/js/modernizr.custom.js",
                        "~/js/jquery.isotope.min.js",
                        "~/js/jquery.magnific-popup.min.js",
                        "~/js/animate.js",
                        "~/js/custom.js",
                        "~/js/owl-carousel/owl.carousel.js"));

            bundles.Add(new StyleBundle("~/template/css").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/fancybox/jquery.fancybox.css",
                    
                      "~/css/flexslider.css",
                   
                      "~/css/style.css"));
            #endregion

        }
    }
}
