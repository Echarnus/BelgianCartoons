using System.Collections.Generic;

namespace BelgianCartoons.Abstract.Models.Twitter
{
    public class Entities
    {
        public IEnumerable<Mention> Mentions { get; set; }
        public IEnumerable<TwitterUrl> Urls { get; set; }
        public IEnumerable<Annotation> Annotations { get; set; }
        public IEnumerable<Hashtag> Hashtags { get; set; }
    }
}