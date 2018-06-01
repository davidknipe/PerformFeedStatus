using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using PerformFeedStatus.Interfaces;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace PerformFeedStatus.Init
{
    [ModuleDependency(typeof(InitializationModule))]
    public class RegisterRoute : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var uiUrl = ServiceLocator.Current.GetInstance<IUrlHelper>().GetRoute();

            RouteTable.Routes.MapRoute(
                name: "PerformFeedRoute",
                url: uiUrl + "PerformFeed/{action}",
                defaults: new { controller = "PerformFeed", action = "FeedStatus" }
            );

            RouteTable.Routes.MapRoute(
                name: "DownloadPerformFeedRoute",
                url: uiUrl + "DownloadPerformFeed/{action}",
                defaults: new { controller = "DownloadPerformFeed", action = "GetFile" }
            );
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
