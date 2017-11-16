using api.Common;
using api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class LearnController : Controller
    {
        private readonly ApiDbContext _context;

        public LearnController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost("api/learn/{userName}")]
        public IActionResult Post(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest();

            var fetcher = new UntappdFetcher();

            _context.Products.AddRange(fetcher.GetUserBeers(userName));

            _context.SaveChanges();

            return Ok();
        }
    }
}
