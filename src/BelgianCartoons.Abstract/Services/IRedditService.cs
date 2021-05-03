using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgianCartoons.Abstract.Services
{
    public interface IRedditService
    {
        Task CreateLinkPostAsync(string subreddit, string title, string url, string flair);
    }
}