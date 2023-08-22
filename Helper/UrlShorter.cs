using System.Linq;

namespace URLShort.Helper
{
    public static class UrlShorter
    {
        public static string GetShortURL(string longUrl)
        {
            if (longUrl == null)
            {
                return "";
            }
            return longUrl.GetHashCode().ToString().Take(8).ToString();
        }

    }
}
