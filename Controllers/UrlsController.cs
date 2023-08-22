using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml;
using URLShort.Helper;
using URLShort.Models;

namespace URLShort.Controllers
{
    [Route("api/urls")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        UrlContext _context;

        public UrlsController(UrlContext db)
        {
            _context = db;
            if(_context.Urls.Any())
            {
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            var urls = _context.Urls.ToList();
            if (urls == null)
            {
                return BadRequest();
            }
            return Ok(urls);
        }

        // GET api/<ShortController>/5
        [HttpGet("{shortUrl}")]
        public ActionResult Get(string shortUrl)
        {
            var url = _context.Urls.FirstOrDefault(s => s.ShortUrl == shortUrl);
            if (url == null)
            {
                return NotFound();
            }
            return Ok(url.LongUrl);
        }

        // POST api/<ShortController>
        [HttpPost("{longUrl}")]
        public ActionResult Post(string longUrl)
        {
            if (longUrl == null)
            {
                return BadRequest();
            }
            var url = _context.Urls.FirstOrDefault(s => s.LongUrl == longUrl);
            if (url == null)
            {
                var shortedUrl = UrlShorter.GetShortURL(longUrl);
                while (_context.Urls.FirstOrDefault(s => s.ShortUrl == shortedUrl) != null)
                {
                    shortedUrl = UrlShorter.GetShortURL(shortedUrl);
                }
                Url newUrl = new Url { LongUrl = longUrl, ShortUrl = shortedUrl };
                _context.Urls.Add(newUrl);
                _context.SaveChanges();
                return Ok(newUrl);
            }
            else
                return Ok(url);

        }

        // DELETE api/<ShortController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var url = _context.Urls.Find(id);
            if (url == null)
            {
                return NotFound();
            }
            return Ok("Succes!");
        }
    }
}
