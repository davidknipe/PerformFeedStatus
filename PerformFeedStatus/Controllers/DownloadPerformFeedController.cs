using System;
using System.Web.Mvc;
using EPiServer.Framework.Blobs;
using EPiServer.Logging;

namespace PerformFeedStatus.Controllers
{
    public class DownloadPerformFeedController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetLogger();
        private readonly IBlobFactory _blobFactory;

        public DownloadPerformFeedController(IBlobFactory blobFactory)
        {
            _blobFactory = blobFactory;
        }

        [Authorize(Roles = "CmsAdmins")]
        public ActionResult GetFile(string fileId)
        {
            try
            {
                byte[] fileBytes = GetFeed(fileId);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Feed.zip");
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in PerformFeedController", ex);
                throw;
            }
        }

        private byte[] GetFeed(string id)
        {
            var blob = _blobFactory.GetBlob(new Uri(id));
            using (var fs = blob.OpenRead())
            {
                byte[] data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);
                return data;
            }
        }
    }
}
