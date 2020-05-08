using System;
using System.Collections.Generic;

namespace CustomVideoIndexer.Models
{
    public class VideoResultInsights
    {
        public string Id { get; set; }
        public string VideoUri { get; set; }
        public string Name { get; set; }

        public List<KeyFrameClean> KeyFrameList { get; set; }
    }

    public class KeyFrameClean
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailId { get; set; }
        public string Labels { get; set; }
        public string CustomLabel { get; set; }
    }

    public class LabelClean
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<AppearanceClean> appearances { get; set; }
    }

    public class AppearanceClean
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }

    public class Duration
    {
        public string time { get; set; }
        public double seconds { get; set; }
    }

    public class Appearance
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public double startSeconds { get; set; }
        public double endSeconds { get; set; }
    }

    public class Keyword
    {
        public bool isTranscript { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public IList<Appearance> appearances { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
        public string language { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class Label
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<Appearance> appearances { get; set; }
        public string referenceId { get; set; }
        public string language { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class SpeakerTalkToListenRatio
    {
    }

    public class SpeakerLongestMonolog
    {
    }

    public class SpeakerNumberOfFragments
    {
    }

    public class SpeakerWordCount
    {
    }

    public class Statistics
    {
        public int correspondenceCount { get; set; }
        public SpeakerTalkToListenRatio speakerTalkToListenRatio { get; set; }
        public SpeakerLongestMonolog speakerLongestMonolog { get; set; }
        public SpeakerNumberOfFragments speakerNumberOfFragments { get; set; }
        public SpeakerWordCount speakerWordCount { get; set; }
    }

    public class SummarizedInsights
    {
        public string name { get; set; }
        public string id { get; set; }
        public string privacyMode { get; set; }
        public Duration duration { get; set; }
        public string thumbnailVideoId { get; set; }
        public string thumbnailId { get; set; }
        public IList<object> faces { get; set; }
        public IList<Keyword> keywords { get; set; }
        public IList<object> sentiments { get; set; }
        public IList<object> emotions { get; set; }
        public IList<object> audioEffects { get; set; }
        public IList<Label> labels { get; set; }
        public IList<object> framePatterns { get; set; }
        public IList<object> brands { get; set; }
        public IList<object> namedLocations { get; set; }
        public IList<object> namedPeople { get; set; }
        public Statistics statistics { get; set; }
        public IList<object> topics { get; set; }
    }

    public class Ocr
    {
        public int id { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string language { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class Instance
    {
        public double confidence { get; set; }
        public string adjustedStart { get; set; }
        public string adjustedEnd { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string duration { get; set; }
        public string thumbnailId { get; set; }
    }

    public class Scene
    {
        public int id { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class KeyFrame
    {
        public int id { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class Shot
    {
        public int id { get; set; }
        public IList<KeyFrame> keyFrames { get; set; }
        public IList<Instance> instances { get; set; }
        public IList<string> tags { get; set; }
    }

    public class VisualContentModeration
    {
        public int id { get; set; }
        public double adultScore { get; set; }
        public double racyScore { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class Block
    {
        public int id { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class Speaker
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<Instance> instances { get; set; }
    }

    public class TextualContentModeration
    {
        public int id { get; set; }
        public int bannedWordsCount { get; set; }
        public int bannedWordsRatio { get; set; }
        public IList<object> instances { get; set; }
    }

    public class Insights
    {
        public string version { get; set; }
        public string duration { get; set; }
        public string sourceLanguage { get; set; }
        public IList<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public IList<string> languages { get; set; }
        public IList<Ocr> ocr { get; set; }
        public IList<Keyword> keywords { get; set; }
        public IList<Label> labels { get; set; }
        public IList<Scene> scenes { get; set; }
        public IList<Shot> shots { get; set; }
        public IList<VisualContentModeration> visualContentModeration { get; set; }
        public IList<Block> blocks { get; set; }
        public IList<Speaker> speakers { get; set; }
        public TextualContentModeration textualContentModeration { get; set; }
        public Statistics statistics { get; set; }
    }

    public class Video
    {
        public string accountId { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string moderationState { get; set; }
        public string reviewState { get; set; }
        public string privacyMode { get; set; }
        public string processingProgress { get; set; }
        public string failureCode { get; set; }
        public string failureMessage { get; set; }
        public object externalId { get; set; }
        public object externalUrl { get; set; }
        public object metadata { get; set; }
        public Insights insights { get; set; }
        public string thumbnailId { get; set; }
        public bool detectSourceLanguage { get; set; }
        public string languageAutoDetectMode { get; set; }
        public string sourceLanguage { get; set; }
        public IList<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public IList<string> languages { get; set; }
        public string indexingPreset { get; set; }
        public string linguisticModelId { get; set; }
        public string personModelId { get; set; }
        public bool isAdult { get; set; }
        public string publishedUrl { get; set; }
        public object publishedProxyUrl { get; set; }
        public string viewToken { get; set; }
    }

    public class Range
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class VideosRanx
    {
        public string videoId { get; set; }
        public Range range { get; set; }
    }

    public class VideoIndex
    {
        public object partition { get; set; }
        public object description { get; set; }
        public string privacyMode { get; set; }
        public string state { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public DateTime created { get; set; }
        public bool isOwned { get; set; }
        public bool isEditable { get; set; }
        public bool isBase { get; set; }
        public int durationInSeconds { get; set; }
        public SummarizedInsights summarizedInsights { get; set; }
        public IList<Video> videos { get; set; }
        public IList<VideosRanx> videosRanges { get; set; }
    }
}
