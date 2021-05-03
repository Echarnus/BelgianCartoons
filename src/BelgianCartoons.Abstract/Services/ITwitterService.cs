using BelgianCartoons.Abstract.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgianCartoons.Abstract.Services
{
    public interface ITwitterService
    {
        Task<List<Tweet>> GetTweetsFromUserAsync(string userId, DateTime startTime, DateTime? endTime = null);
    }
}