﻿using System.Web;
using System.Web.Optimization;

namespace AspNetMVC
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/lib/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/js/lib/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/js/lib/jquery.dataTables.min.js",
                        "~/js/lib/dataTables.bootstrap.min.js",
                        "~/js/lib/dataTables.colReorder.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cleanBlog").Include(
                        "~/js/lib/clean-blog.min.js"));

           bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/css/bootstrap-theme.min.css",
                        "~/css/bootstrap.min.css",
                        "~/css/jquery.dataTables.min.css",
                        "~/css/jquery.dataTables_themeroller.css",
                        "~/css/dataTables.bootstrap.min.css",
                        "~/css/colReorder.bootstrap.min.css",
                        "~/css/font-awesome.min.css",
                        "~/css/clean-blog.min.css",
                        "~/css/PagedList.css",
                        "~/css/site.css"));
        }
    }
}
