﻿using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Controllers
{
    public class BestBeerController : Controller
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
                        ColumnNames = new [] { "Rating", "Ibu", "Abv", "Ipa", "Ale", "Pale", "American", "Lager", "Imperial", "Stout" },
                        Values = new [] { new [] { "0", "0", "4", "1", "0", "0", "0", "0", "0", "0" }, new[] { "0", "0", "5", "0", "1", "0", "0", "0", "0", "0" } }
                    }
                },
                GlobalParameters = new {}
            };

            var apiKey = "tlggDKQ79w5f+1p4AF+J6ZOg3hX0hnvY6ZXkG27vz6c7rmrFzseNhb5ygfNW/XE2jo17OYxTxyor5NwQQqGF7w==";

            var response = await RestClient.For<IAzureMlRestApi>("https://ussouthcentral.services.azureml.net")
                .GetUserAsync("328c4954b6cb490bb50e9e66f8c3553c", "0e2bc37b345b4172b6f15410ffeaf8af", foo, $"Bearer {apiKey}");

            var values = (JArray) response["Results"]["output1"]["value"]["Values"];
            var bestOfTheBest = double.Parse(values[0][10].ToString(), CultureInfo.InvariantCulture) > double.Parse(values[1][10].ToString(), CultureInfo.InvariantCulture) 
                ? "Kujan IPA" : "South Pacific Pale Ale";

            return Ok(new
            {
                AndTheWinnerIs = bestOfTheBest
            });
        }
    }
}
