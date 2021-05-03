using System;

namespace BelgianCartoons.Abstract.Models.Twitter
{
    public class Tweet
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public Entities Entities { get; set; }
        public DateTime Created_At { get; set; }
    }
}