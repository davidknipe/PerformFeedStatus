using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Data;
using EPiServer.Logging;
using EPiServer.Personalization.Commerce.CatalogFeed.Internal;
using EPiServer.PlugIn;
using PerformFeedStatus.Interfaces;
using PerformFeedStatus.Models.ViewModels;

namespace PerformFeedStatus.Controllers
{
    [GuiPlugIn(
        Area = PlugInArea.AdminMenu,
        Url = "/episerver/PerformFeedStatus/PerformFeed",
        DisplayName = "Peform Feed Status")]
    public class PerformFeedController : Controller
    {
        public const string ViewRootPath = "~/modules/_protected/PerformFeed.UI/Views/{0}";
        private static readonly ILogger Logger = LogManager.GetLogger();
        private readonly CatalogFeedTokenStore _tokenStore;
        private readonly IUrlHelper _urlHelper;

        public PerformFeedController(CatalogFeedTokenStore tokenStore, IUrlHelper urlHelper)
        {
            _tokenStore = tokenStore;
            _urlHelper = urlHelper;
        }

        [Authorize(Roles = "CmsAdmins")]
        public ActionResult FeedStatus()
        {
            try
            {
                string viewPath = String.Format(ViewRootPath, "FeedStatus.cshtml");

                var model = GetModel();

                if(model.FeedTokens == null || !model.FeedTokens.Any())
                {
                    model.ConfirmationMessage = "There are no outstanding feed exports";
                }

                return View(viewPath, model);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in PerformFeedController", ex);
                throw;
            }
        }

        private FeedStatusViewModel GetModel()
        {
            var model = new FeedStatusViewModel {FeedTokens = new List<FeedToken>()};

            //WARNING: Reflection and accessing internal namespaces goes here...
            var t = _tokenStore.GetType();
            var mi = t.GetMethod("LoadAll", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
            {
                var result = mi.Invoke(_tokenStore, null) as IEnumerable<object>;
                if (result != null)
                    foreach (var token in result)
                    {
                        var blobId = token.GetType().GetProperty("BlobId").GetValue(token) as Uri;
                        var expires = token.GetType().GetProperty("Expires").GetValue(token) as DateTime?;
                        var id = token.GetType().GetProperty("Id").GetValue(token) as Identity;

                        model.FeedTokens.Add(new FeedToken()
                        {
                            BlobId = blobId,
                            Expires = expires.Value,
                            Id = id,
                            BlobUrl = _urlHelper.GetDownloadUrl() + "?fileid=" + blobId
                        });
                    }
            }

            return model;
        }
    }
}
