using System.Web.Optimization;
using Northwind.WebClientMvc4;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(JqueryBundleConfig), "RegisterBundles")]

namespace Northwind.WebClientMvc4
{
    public class JqueryBundleConfig
    {
        public static void RegisterBundles()
        {
            // Add @Scripts.Render("~/bundles/jquery") before bootstrap or angular in your _Layout.cshtml view
            // When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery*"));
        } 
    }
}