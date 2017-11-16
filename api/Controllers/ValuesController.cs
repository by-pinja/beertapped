using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Controllers
{
    public class ValuesController : Controller
    {
        [HttpGet("api/bestbeer/{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest();

            var foo = new
            {
                Inputs = new
                {
                    input1 = new
                    {
                        ColumnNames = new [] { "beer_abv", "rating", "beer_ibu", "style" },
                        Values = new [] { new [] { "0", "0", "0", "IPA" }, new[] { "0", "0", "0", "Ale" } }
                    }
                },
                GlobalParameters = new {}
            };

            var response = await RestClient.For<IAzureMlRestApi>("https://ussouthcentral.services.azureml.net")
                .GetUserAsync(foo, "Bearer fsenvvHHnkmH/B5FDyeIjUgGQHusbYrJrTHQwqAdFRFINFbvtE3/HoSLW9V1xljuShlNIwlTNoWrMhWH1hTORQ==");

            var values = (JArray) response["Results"]["output1"]["value"]["Values"];
            var bestOfTheBest = double.Parse(values[0][4].ToString(), CultureInfo.InvariantCulture) > double.Parse(values[1][4].ToString(), CultureInfo.InvariantCulture) 
                ? "Kujan IPA" : "South Pacific Pale Ale";

            return Ok(new
            {
                AndTheWinnerIs = bestOfTheBest
            });
        }
    }
}
