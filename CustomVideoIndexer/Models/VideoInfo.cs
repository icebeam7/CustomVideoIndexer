using System;
using System.Collections.Generic;

namespace CustomVideoIndexer.Models
{
    public class VideoResultClean
    {
        public string id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string thumbnailId { get; set; }
    }
    
    public class VideoResult
    {
        public string accountId { get; set; }
        public string id { get; set; }
        public object partition { get; set; }
        public object externalId { get; set; }
        public object metadata { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public DateTime created { get; set; }
        public DateTime lastModified { get; set; }
        public DateTime lastIndexed { get; set; }
        public string privacyMode { get; set; }
        public string userName { get; set; }
        public bool isOwned { get; set; }
        public bool isBase { get; set; }
        public bool hasSourceVideoFile { get; set; }
        public string state { get; set; }
        public string moderationState { get; set; }
        public string reviewState { get; set; }
        public string processingProgress { get; set; }
        public int durationInSeconds { get; set; }
        public string thumbnailVideoId { get; set; }
        public string thumbnailId { get; set; }
        public IList<object> searchMatches { get; set; }
        public string indexingPreset { get; set; }
        public string streamingPreset { get; set; }
        public string sourceLanguage { get; set; }
        public IList<string> sourceLanguages { get; set; }
        public string personModelId { get; set; }
    }

    public class NextPage
    {
        public int pageSize { get; set; }
        public int skip { get; set; }
        public bool done { get; set; }
    }

    public class VideoInfo
    {
        public IList<VideoResult> results { get; set; }
        public NextPage nextPage { get; set; }
    }
}
