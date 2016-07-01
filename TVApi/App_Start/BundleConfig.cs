using System.Web;
using System.Web.Optimization;

namespace TVApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                  "~/Scripts/jquery.signalR-2.2.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-1.6.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr/hubs").Include(
                  "~/signalr/hubs"));
        }
    }
}
