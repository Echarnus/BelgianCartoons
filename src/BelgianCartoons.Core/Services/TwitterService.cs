using BelgianCartoons.Abstract.Models.Twitter;
using BelgianCartoons.Abstract.Services;
using BelgianCartoons.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelgianCartoons.Core.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly HttpClient _httpClient;
        private readonly TwitterSettings _twitterSettings;

        public TwitterService(HttpClient httpClient, TwitterSettings twitterSettings)
        {
            this._httpClient = httpClient;
            this._twitterSettings = twitterSettings;
        }

        public async Task<List<Tweet>> GetTweetsFromUserAsync(string userId, DateTime startTime, DateTime? endTime = null)
        {
            var tweets = new List<Tweet>();
            var fetchedTweets = await GetTweetsSinceAsync(userId, startTime, null);
            if (fetchedTweets?.Data?.Count() > 0)
            {
                tweets.AddRange(fetchedTweets.Data);
                DateTime lowest = tweets.Min(tweet => tweet.Created_At);
                while (lowest > startTime && fetchedTweets != null && fetchedTweets?.Data?.Count() == 100)
                {
                    fetchedTweets = await GetTweetsSinceAsync(userId, startTime, lowest);
                    if (fetchedTweets?.Data != null)
                    {
                        tweets.AddRange(fetchedTweets.Data);
                        lowest = fetchedTweets.Data.Min(tweet => tweet.Created_At);
                    }
                }
            }
            return tweets.OrderBy((tweet) => tweet.Created_At).ToList();
        }

        public async Task<string> GetUserIdByName(string userName)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {_twitterSettings.Token}");
            httpRequestMessage.RequestUri = new Uri($"https://api.twitter.com/2/users/by/username/{userName}");
            var result = await _httpClient.SendAsync(httpRequestMessage);
            var content = await result.Content.ReadAsStringAsync();
            return string.Empty;
        }

        private static string FormatRFC3339(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ");
        }

        private async Task<Tweets> GetTweetsSinceAsync(string userId, DateTime startTime, DateTime? endTime = null)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {_twitterSettings.Token}");

            if (endTime != null)
            {
                httpRequestMessage.RequestUri = new Uri($"https://api.twitter.com/2/users/{userId}/tweets?max_results=100&start_time={FormatRFC3339(startTime)}&end_time={FormatRFC3339(endTime.Value)}&tweet.fields=entities,created_at");
            }
            else
            {
                httpRequestMessage.RequestUri = new Uri($"https://api.twitter.com/2/users/{userId}/tweets?max_results=100&start_time={FormatRFC3339(startTime)}&tweet.fields=entities,created_at");
            }

            var result = await _httpClient.SendAsync(httpRequestMessage);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Tweets>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}