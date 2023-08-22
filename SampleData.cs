using URLShort.Models;

namespace URLShort
{
    public class SampleData
    {
        public static void Initialize(UrlContext context, UserContext userContext)
        {
            if (!context.Urls.Any())
            {
                context.AddRange(
                    new Url { LongUrl = "https://www.youtube.com/", ShortUrl = "test1", UserId = 0, CreatedDate=DateTime.Now},
                    new Url { LongUrl = "https://104.ua/", ShortUrl = "test2", UserId = 1, CreatedDate = DateTime.Now },
                    new Url { LongUrl = "https://mail.google.com/", ShortUrl = "test3", UserId = 0, CreatedDate = DateTime.Now }); 
                context.SaveChanges();
            }
        }

    }
}
