using System.Web.Optimization;
using Northwind.WebClientMvc4;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(CustomerAdminBundle), "RegisterBundles")]


namespace Northwind.WebClientMvc4
{

    public class CustomerAdminBundle
    {
        public static void RegisterBundles()
        {
            // Add @Scripts.Render("~/bundles/customerAdmin") after other bundles in your *.cshtml view
            // When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/customerAdmin")
                .Include("~/Assets/js/CustomerAdmin/*.js")
                .Include("~/Assets/js/ng-bootstrap.js")
                );
        }
    }
}