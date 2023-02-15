using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebShortCut.Models;

public class AppDbContext : IdentityDbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options) {
	}

	public DbSet<Link> Links { get; set; }

	public string LinkLogic(string link, Guid currUserId) {
		var url = Links.FirstOrDefault(x => x.UserId == currUserId &&  x.Url == link);
		if(url != null) {
			return url.Short;
		}

		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var random = new Random();
		var result = new string(
			Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

		Links.Add(new Link {
			Url = link,
			Short = result,
			UserId = currUserId
		});
		SaveChanges();
		return result;
	}

	public void AddSeedData() {
		var link1 = new Link {
			Id = 1,
			Url = "https://sela.co.il",
			Short = "sela"
		};
		var link2 = new Link {
			Id = 2,
			Url = "https://google.co.il",
			Short = "g"
		};

		Links.AddRange(link1, link2);
		SaveChanges();
	}

}
