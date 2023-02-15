using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WebShortCut.Models;

namespace WebShortCut.Controllers {
	public class HomeController : Controller {
		private readonly AppDbContext appDb;
		private readonly ILogger<HomeController> _logger;

		public HomeController(AppDbContext db, ILogger<HomeController> logger) {
			appDb = db;
			_logger = logger;
		}

		public IActionResult Index() {
			var cnt = appDb.Links.Count();

			return View(cnt);
		}
		//[HttpPost]
		//public IActionResult Index(LinkShortCut model)
		//      {
		//          var shortUrl = "ofekTest";
		//          ViewData[shortUrl] = model.Id;
		//          return View(model);
		//      }

		public IActionResult Privacy() {
			return View();
		}

        [HttpGet("/s/{shorturl}")]
        public ActionResult Get(string shorturl) {
            var url = appDb.Links.FirstOrDefault(u => u.Short == shorturl);
            if(url != null) {
				url.Count++;
				appDb.SaveChanges();

                return Redirect(url.Url);
            }
            return NotFound();
        }


        public IActionResult LinkShortCut() {
            return View();
        }

        [Authorize]
        public IActionResult Links() {
			var data = appDb.Links.ToList();
			return View(data);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}