namespace URLShort.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
    }
}
