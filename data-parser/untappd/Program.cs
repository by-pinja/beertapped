using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json.Linq;
using RestEase;

namespace untappd
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fetch data from Untappd

            var response = GetData().Result["response"];
            var totalCount = double.Parse(response["total_count"].ToString(), CultureInfo.InvariantCulture);

            var userBeers = ParseResponse(response);
            
            for (var offset = 50; offset < totalCount; offset += 50)
            {
                response = GetData(offset).Result["response"];
                userBeers.AddRange(ParseResponse(response));
            }

            // Convert Beer to CSV
            WriteCsv(userBeers);
        }

        private static void WriteCsv(List<Beer> userBeers)
        {
            var csvItems = userBeers.Where(m => m.Rating > 0).Select(m => new CsvBeer
            {
                Abv = m.Abv.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                Ibu = m.Ibu.ToString(),
                Rating = m.Rating.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                Ale = m.Styles.Contains("Ale") ? "1" : "0",
                American = m.Styles.Contains("American") ? "1" : "0",
                Ipa = m.Styles.Contains("Ipa") ? "1" : "0",
                Lager = m.Styles.Contains("Lager") ? "1" : "0",
                Pale = m.Styles.Contains("Pale") ? "1" : "0",
                Imperial = m.Styles.Contains("Imperial") ? "1" : "0",
                Stout = m.Styles.Contains("Stout") ? "1" : "0"
            });

            var testWrite = File.CreateText(@"output.csv");

            var csvWriter = new CsvWriter(testWrite);

            csvWriter.WriteRecords(csvItems);

            csvWriter.Flush();

            testWrite.Flush();
        }

        static async Task<JObject> GetData(int offset = 0)
        {
            return await RestClient.For<IUntappdRestApi>("https://api.untappd.com")
                .GetUserAsync("ajaska", "13A8DEAF7B8F0272256990BB7F72169C1F37E7C3", 0);
        }

        static List<Beer> ParseResponse(dynamic response)
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



    public class Beer
    {
        public double Rating { get; set; }

        public int Ibu { get; set; }

        public double Abv { get; set; }

        public string[] Styles { get; set; }
    }

    public class CsvBeer
    {
        public string Rating { get; set; }

        public string Ibu { get; set; }

        public string Abv { get; set; }

        public string Ipa { get; set; }

        public string Ale { get; set; }

        public string Pale { get; set; }

        public string American { get; set; }

        public string Lager { get; set; }

        public string Imperial { get; set; }

        public string Stout { get; set; }

    }
}
