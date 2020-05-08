using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;

using CustomVideoIndexer.Helpers;
using CustomVideoIndexer.Models;
using System.Globalization;

namespace CustomVideoIndexer.Controllers
{
    public class VideoInfoController : Controller
    {
        private async Task<string> GetToken()
        {
            var token = string.Empty;

            var tokenResponse = await clientAuth.GetAsync(Constants.TokenService);

            if (tokenResponse.IsSuccessStatusCode)
            {
                var content = await tokenResponse.Content.ReadAsStringAsync();
                token = content.Substring(1, content.Length - 2);
            }

            return token;
        }

        public async Task<ActionResult> Index()
        {
            if (string.IsNullOrWhiteSpace(Constants.VideoIndexerAccessToken))
                Constants.VideoIndexerAccessToken = await GetToken();

            List<VideoResultClean> results = new List<VideoResultClean>();

            var response = await client.GetAsync($"{Constants.ListVideos}{Constants.VideoIndexerAccessToken}");

            if (response.IsSuccessStatusCode)
            {
                var videoResponse = await response.Content.ReadAsStringAsync();
                var videoInfo = JsonConvert.DeserializeObject<VideoInfo>(videoResponse);

                if (videoInfo.results.Count > 0)
                {
                    foreach (var item in videoInfo.results)
                    {
                        var thumbnail = await GetThumbnail(item.id, item.thumbnailId);

                        results.Add(new VideoResultClean()
                        {
                            id = item.id,
                            name = item.name,
                            thumbnailId = item.thumbnailId,
                            thumbnail = $"data:image/png;base64,{thumbnail}"
                        });
                    }

                }
            }

            return View(results);
        }

        private async Task<string> GetThumbnail(string videoId, string thumbnailId)
        {
            var thumbnailResponse = await client.GetAsync($"Videos/{videoId}/Thumbnails/{thumbnailId}?format=Base64&accessToken={Constants.VideoIndexerAccessToken}");

            return thumbnailResponse.IsSuccessStatusCode
                ? await thumbnailResponse.Content.ReadAsStringAsync()
                : string.Empty;
        }

        // GET: VideoInfo/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var insights = new VideoResultInsights();
            insights.Id = id;
            insights.KeyFrameList = new List<KeyFrameClean>();

            var labels = new List<Label>();

            var downloadUriResponse = await client.GetAsync($"Videos/{id}/SourceFile/DownloadUrl?accessToken={Constants.VideoIndexerAccessToken}");

            if (downloadUriResponse.IsSuccessStatusCode)
            {
                var url = await downloadUriResponse.Content.ReadAsStringAsync();
                insights.VideoUri = url.Substring(1, url.Length - 2);
            }

            var indexResponse = await client.GetAsync($"Videos/{id}/Index?reTranslate=False&includeStreamingUrls=True?accessToken={Constants.VideoIndexerAccessToken}");

            if (indexResponse.IsSuccessStatusCode)
            {
                var indexContent = await indexResponse.Content.ReadAsStringAsync();
                var videoIndex = JsonConvert.DeserializeObject<VideoIndex>(indexContent);

                if (videoIndex != null)
                {
                    var video = videoIndex.videos.FirstOrDefault();

                    if (video != null)
                    {
                        foreach (var shot in video.insights.shots)
                        {
                            foreach (var keyFrame in shot.keyFrames)
                            {
                                foreach (var instance in keyFrame.instances)
                                {
                                    var thumbnail = await GetThumbnail(id, instance.thumbnailId);

                                    insights.KeyFrameList.Add(new KeyFrameClean()
                                    {
                                        Start = instance.start,
                                        End = instance.end,
                                        ThumbnailId = instance.thumbnailId,
                                        Thumbnail = $"data:image/png;base64,{thumbnail}"
                                    });
                                }
                            }
                        }

                        labels = video.insights.labels.ToList();
                    }
                }
            }

            var labelsClean = new List<LabelClean>();

            foreach (var item in labels)
            {
                var ac = new List<AppearanceClean>();

                if (item.instances != null)
                {
                    foreach (var app in item.instances)
                    {
                        ac.Add(new AppearanceClean()
                        {
                            StartTime = ConvertTime(app.start),
                            EndTime = ConvertTime(app.end)
                        });
                    }
                }

                labelsClean.Add(new LabelClean()
                {
                    name = item.name,
                    id = item.id,
                    appearances = ac
                });
            }

            foreach (var item in insights.KeyFrameList)
            {
                var startTime = ConvertTime(item.Start);
                var endTime = ConvertTime(item.End);

                foreach (var label in labelsClean)
                {
                    if (label.appearances.Any(x => startTime >= x.StartTime && endTime <= x.EndTime))
                        item.Labels += $"{label.name},  ";
                }


                var base64Image = item.Thumbnail.Replace("data:image/png;base64,", string.Empty);
                var predictions = await DetectObjects(base64Image);

                foreach (var p in predictions)
                {
                    item.CustomLabel += $"{ p.TagName }, ";
                }

                if (!string.IsNullOrWhiteSpace(item.CustomLabel))
                    item.CustomLabel = $"/ Custom Label detected: {item.CustomLabel}";
            }

            return View(insights);
        }

        private async static Task<List<Prediction>> DetectObjects(string base64Image)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64Image);
                var content = new StreamContent(new MemoryStream(bytes));

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await clientCV.PostAsync(Constants.CustomVisionService, content);

                if (response.IsSuccessStatusCode)
                {
                    var predictionResult = await response.Content.ReadAsStringAsync();
                    var customVisionResult = JsonConvert.DeserializeObject<CustomVisionResult>(predictionResult);

                    return customVisionResult.Predictions.Where(x => x.Probability > 0.1).ToList();
                }
                else
                {
                    return new List<Prediction>();
                }
            }
            catch (Exception ex)
            {
                return new List<Prediction>();
            }
        }

        private static readonly HttpClient client =
            CreateHttpClient($"{Constants.VideoIndexerBaseURL}/{Constants.AccountID}/",
                "Ocp-Apim-Subscription-Key",
                Constants.SubscriptionKey);

        private static readonly HttpClient clientCV =
            CreateHttpClient(Constants.CustomVisionBaseUrl,
                "Prediction-Key",
                Constants.CustomVisionKey);

        private static readonly HttpClient clientAuth =
            CreateHttpClient($"{Constants.AuthBaseURL}/{Constants.AccountID}/",
                "Ocp-Apim-Subscription-Key",
                Constants.SubscriptionKey);

        private static HttpClient CreateHttpClient(string url, string headerKey, string key)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(headerKey))
                client.DefaultRequestHeaders.Add(headerKey, key);

            return client;
        }

        private double ConvertTime(string time)
        {
            var culture = CultureInfo.CurrentCulture;
            var format = "H:mm:ss.f"; //0:00:02.7

            if (time.Length < 9)
                time += ".0";

            return DateTime.ParseExact(time.Substring(0, 9), format, culture).TimeOfDay.TotalSeconds;
        }

    }
}