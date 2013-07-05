using System.Web.Optimization;
using Northwind.WebClientMvc4;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(UnderscoreBundleConfig), "RegisterBundles")]

namespace Northwind.WebClientMvc4
{
    public class UnderscoreBundleConfig
    {
        public static void RegisterBundles()
        {
            // Add @Scripts.Render("~/bundles/underscore") in your .cshtml view
            // When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/underscore").Include("~/Scripts/underscore*"));
        } 
    }
}