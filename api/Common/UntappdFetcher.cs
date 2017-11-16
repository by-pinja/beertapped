using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using api.Domain;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Common
{
    public class UntappdFetcher
    {
        public List<BeerStyleRatingModel> GetUserBeers(string userName)
        {
            // Fetch data from Untappd
            var response = GetData(userName).Result["response"];
            var totalCount = double.Parse(response["total_count"].ToString(), CultureInfo.InvariantCulture);

            var userBeers = ParseResponse(response);

            for (var offset = 50; offset < totalCount; offset += 50)
            {
                response = GetData(userName, offset).Result["response"];
                userBeers.AddRange(ParseResponse(response));
            }

            var models = userBeers.Where(m => m.Rating > 0).Select(m => new BeerStyleRatingModel
            {
                User = userName,
                Abv = m.Abv.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                Ibu = m.Ibu.ToString(),
                Rating = m.Rating,
                Ale = m.Styles.Contains("Ale") ? "1" : "0",
                American = m.Styles.Contains("American") ? "1" : "0",
                Ipa = m.Styles.Contains("Ipa") ? "1" : "0",
                Lager = m.Styles.Contains("Lager") ? "1" : "0",
                Pale = m.Styles.Contains("Pale") ? "1" : "0",
                Imperial = m.Styles.Contains("Imperial") ? "1" : "0",
                Stout = m.Styles.Contains("Stout") ? "1" : "0"
            }).ToList();

            return models;
        }

        private async Task<JObject> GetData(string userName, int offset = 0)
        {
            return await RestClient.For<IUntappdRestApi>("https://api.untappd.com")
                .GetUserAsync(userName, "13A8DEAF7B8F0272256990BB7F72169C1F37E7C3", offset);
        }

        private static List<Beer> ParseResponse(dynamic response)
        {
            var userBeers = new List<Beer>();

            var items = response.beers.items;

            foreach (var item in items)
            {
                string style = item.beer.beer_style;
                userBeers.Add(new Beer
                {
                    Rating = item.rating_score,
                    Styles = style.Split(' '),
                    Abv = item.beer.beer_abv,
                    Ibu = item.beer.beer_ibu
                });
            }

            return userBeers;
        }

    }
}
