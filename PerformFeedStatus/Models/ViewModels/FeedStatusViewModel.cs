using System;
using System.Collections.Generic;
using EPiServer.Data;

namespace PerformFeedStatus.Models.ViewModels
{
    public class FeedStatusViewModel
    {
        public string ConfirmationMessage { get; set; }
        public List<FeedToken> FeedTokens { get; set; }
    }

    public class FeedToken
    {
        public Uri BlobId { get; set; }

        public DateTime Expires { get; set; }

        public Identity Id { get; set; }

        public string BlobUrl { get; set; }
    }
}
