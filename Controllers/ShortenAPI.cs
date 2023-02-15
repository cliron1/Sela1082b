using Microsoft.AspNetCore.Mvc;
using WebShortCut.Models;

namespace WebShortCut.Controllers {
    [ApiController]
    [Route("api")]
    public class ShortenAPI : ControllerBase {
        private ILogger<AppDbContext> logger;
        private AppDbContext appDb;

        public ShortenAPI(AppDbContext db, ILogger<AppDbContext> logger) {
            this.logger = logger;
            this.appDb = db;
        }

        [HttpPost("short-link")]
        public IActionResult ShortLink([FromBody] string link) {
            if(string.IsNullOrEmpty(link)) {
                return BadRequest();
            }

            //var ret = Url.Action(nameof(Get), "ShortenAPI", new { shorturl = shortUrl });
            var shortUrl = appDb.LinkLogic(link, Guid.Empty);

            var fullUrl = $"{HttpContext.GetFullDomain()}/s/{shortUrl}";

            var ret = new {
                Url = link,
                Short = fullUrl
            };

            return Ok(ret);
        }

        //private string getDomain() {
        //	var req = HttpContext.Request;
        //	return $"{req.Scheme}://{req.Host}";
        //}
    }
}
