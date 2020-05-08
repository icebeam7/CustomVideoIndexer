using System;
namespace CustomVideoIndexer.Helpers
{
    public class Constants
    {
        public static readonly string SubscriptionKey = "";
        public static readonly string AccountID = "";
        public static readonly string VideoIndexerBaseURL = "https://api.videoindexer.ai/trial/Accounts";
        public static readonly string AuthBaseURL = "https://api.videoindexer.ai/Auth/trial/Accounts/";
        public static readonly string TokenService = $"AccessToken?allowEdit=False";
        public static readonly string ListVideos = "Videos?pageSize=25&skip=0&accessToken=";

        public static readonly string CustomVisionBaseUrl = "";
        public static readonly string CustomVisionKey = "";
        public static readonly string CustomVisionService = "";

        public static string VideoIndexerAccessToken = "";
    }
}
