﻿using System.Web;
using System.Web.Optimization;

namespace LongKhanhMobile
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Danh cho trang Admin
            bundles.Add(new ScriptBundle("~/bundles/admmainjs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/plugins/metismenu/jquery.metisMenu.js",
                "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/admjqueryval").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/admcustomjs").Include(
               "~/Scripts/cheapdeal.js",
               "~/Scripts/plugins/pace/pace.min.js"
               ));

            bundles.Add(new StyleBundle("~/Content/admbootstrap").Include(
                "~/Content/bootstrap-3.3.0.min.css",
                "~/Content/font-awesome.css"
                ));

            bundles.Add(new StyleBundle("~/Content/admstyle").Include(
                "~/Content/animate.css",
                "~/Content/style.css",
                "~/Content/AdmSite.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/masterslider.css",
                       "~/Content/styles.css",
                      "~/Content/color-default.css",
                      "~/Content/font-awesome.css",
                      "~/Content/color-switcher.css", 
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/libraryscript").Include(
                        "~/Scripts/jquery-1.11.1.min.js",
                        "~/Scripts/jquery-ui-1.10.4.custom.min.js",
                        "~/Scripts/jquery.easing.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/smoothscroll.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/icheck.min.js",
                        "~/Scripts/jquery.placeholder.js",
                        "~/Scripts/jquery.stellar.min.js",
                        "~/Scripts/jquery.touchSwipe.min.js",
                        "~/Scripts/jquery.shuffle.min.js",
                        "~/Scripts/lightGallery.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/masterslider.min.js",
                        "~/Scripts/mailer.js",
                        "~/Scripts/scripts.js",
                        "~/Scripts/color-switcher.js"));

        }
    }
}
