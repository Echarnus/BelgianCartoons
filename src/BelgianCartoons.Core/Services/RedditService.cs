using BelgianCartoons.Abstract.Services;
using BelgianCartoons.Core.Settings;
using Reddit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgianCartoons.Core.Services
{
    public class RedditService : IRedditService
    {
        private readonly RedditClient _redditClient;
        private readonly RedditSettings _redditSettings;

        public RedditService(RedditSettings redditSettings)
        {
            this._redditSettings = redditSettings;
            _redditClient = new RedditClient(appId: _redditSettings.AppId, refreshToken: _redditSettings.RefreshToken);
        }

        public async Task CreateLinkPostAsync(string subreddit, string title, string url, string flair)
        {
            var subredditObject = _redditClient.Subreddit(subreddit);
            var linkPost = subredditObject.LinkPost(title: title, url: url);
            linkPost = await linkPost.SubmitAsync().ConfigureAwait(false);
            linkPost.SetFlair(flair);
        }
    }
}