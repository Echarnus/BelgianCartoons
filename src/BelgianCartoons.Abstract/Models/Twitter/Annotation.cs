namespace BelgianCartoons.Abstract.Models.Twitter
{
    public class Annotation : Entity
    {
        public float Probability { get; set; }
        public string Type { get; set; }
        public string Normalized_text { get;set;}
    }
}