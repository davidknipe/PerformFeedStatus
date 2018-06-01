using EPiServer.Framework.Modules;
using EPiServer.ServiceLocation;
using PerformFeedStatus.Interfaces;

namespace PerformFeedStatus.Impl
{
    [ServiceConfiguration(typeof(IUrlHelper))]
    public class UrlHelper : IUrlHelper
    {
        private readonly IModuleResourceResolver _moduleResourceResolver;

        public UrlHelper(IModuleResourceResolver moduleResourceResolver)
        {
            _moduleResourceResolver = moduleResourceResolver;
        }

        public string GetRoute()
        {
            var uiUrl = _moduleResourceResolver.ResolvePath("CMS", string.Empty).TrimStart("/".ToCharArray());
            uiUrl = uiUrl.Replace("/CMS", string.Empty);
            uiUrl += "PerformFeedStatus/";

            return uiUrl;
        }

        public string GetDownloadUrl()
        {
            return "/" + GetRoute() + "DownloadPerformFeed/";
        }
    }
}
