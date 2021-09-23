using BelgianCartoons.Abstract.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BelgianCartoons.Scraper.Functions.Triggers
{
    public class GetCartoonistsFromTwitter
    {

        private const string SCHEDULE_EXPRESSION = "* * */6 * * *";
        private readonly ILogger<GetCartoonistsFromTwitter> _logger;
        private readonly IRedditService _redditService;
        private readonly ITwitterService _twitterService;
        private readonly DateTime MINIMUM_DATE = new DateTime(2021, 9, 15); // Use this var to avoid blasting my Twitter limit

        public GetCartoonistsFromTwitter(ITwitterService twitterService, IRedditService redditService, ILogger<GetCartoonistsFromTwitter> logger)
        {
            _twitterService = twitterService;
            _redditService = redditService;
            _logger = logger;
        }

        [FunctionName("GetSoupCatCartoons")]
        public async Task RunGeSoupCatCartoons([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("25047751", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "comic", StringComparison.OrdinalIgnoreCase) || string.Equals(t?.Tag, "comics", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "Soupcat");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text.Split("\n")[0]}", $"https://twitter.com/soupcats/status/{cartoon.Id}?", "Soupcat");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Soupcat");
                }
            }
        }

        [FunctionName("GetArnulfBe")]
        public async Task RunGetArnulfBe([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("249821479", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "cartoon", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "ArnulfBE");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text.Split("\n")[0]}", $"https://twitter.com/arnulfcartoon/status/{cartoon.Id}?", "Arnulf");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Arnulf");
                }
            }
        }

        [FunctionName("GetCanaryPete")]
        public async Task RunGetCanaryPete([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("2605192496", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "cartoon", StringComparison.OrdinalIgnoreCase) || string.Equals(t?.Tag, "comics", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "CanaryPete");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text.Split("\n")[0]}", $"https://twitter.com/canarypete2/status/{cartoon.Id}?", "CanaryPete");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "CanaryPete");
                }
            }
        }

        [FunctionName("GetFaudtnieuws")]
        public async Task RunGetFaudtNieuws([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("3257053183", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "faudtnieuws", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "FaudtNieuws");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text.Split("\n")[0]}", $"https://twitter.com/faudtnieuws/status/{cartoon.Id}?", "FaudtNieuws");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "FaudtNieuws");
                }
            }
        }

        [FunctionName("GetJeroomCartoons")]
        public async Task RunGetJeroomCartoons([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("27617210", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Text.Contains("Jeroom", StringComparison.OrdinalIgnoreCase));
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "Jeroom");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text}", $"https://twitter.com/Humo/status/{cartoon.Id}?", "Jeroom");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Jeroom");
                }
            }
        }

        [FunctionName("GetKimDuchateau")]
        public async Task RunGetKimDuchateau([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var cartoonTweets = await _twitterService.GetTweetsFromUserAsync("3351533926", lastUpdated).ConfigureAwait(false);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "KimDuchateau");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text}", $"https://twitter.com/kim_duchateau/status/{cartoon.Id}", "KimDuchateau");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Jeroom");
                }
            }
        }

        [FunctionName("GetLectrrCartoons")]
        public async Task RunGetLectrrCartoons([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("13157492", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "cartoon", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "Lectrr");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text}", $"https://twitter.com/lectrr/status/{cartoon.Id}?", "Lectrr");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Lectrr");
                }
            }
        }

        [FunctionName("GetVanmoltoons")]
        public async Task RunGetVanmolCartoons([TimerTrigger(SCHEDULE_EXPRESSION)] TimerInfo myTimer)
        {
            var lastUpdated = myTimer?.ScheduleStatus?.Last ?? MINIMUM_DATE;
            _logger.LogInformation("Last SyncTime was {SyncTime}", lastUpdated);

            var tweets = await _twitterService.GetTweetsFromUserAsync("34889535", lastUpdated).ConfigureAwait(false);
            var cartoonTweets = tweets.Where(tweet => tweet.Entities?.Hashtags?.Any(t => string.Equals(t?.Tag, "cartoon", StringComparison.OrdinalIgnoreCase)) == true);
            _logger.LogInformation("Found {TotalTweets} Tweets for cartoonist {Cartoonist}", cartoonTweets.Count(), "Vanmoltoons");
            foreach (var cartoon in cartoonTweets)
            {
                try
                {
                    await _redditService.CreateLinkPostAsync("BelgianCartoons", $"{cartoon.Text}", cartoon.Entities.Urls.FirstOrDefault()?.Expanded_url, "Vanmoltoons");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem with processing Tweet {Tweet} for cartoonist {Cartoonist}", cartoon.Id, "Vanmoltoons");
                }
            }
        }
    }
}