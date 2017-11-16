using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Newtonsoft.Json.Linq;

namespace untappd
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText(@"data.json");

            dynamic yourObject = JObject.Parse(json);

            var objects = new List<Beer>();

            var all = yourObject.all;

            foreach (var groupOfData in all)
            {
                var items = groupOfData.response.beers.items;

                foreach (var item in items)
                {
                    string style = item.beer.beer_style;
                    objects.Add(new Beer
                    {
                        Rating = item.rating_score,
                        Styles = style.Split(' '),
                        Abv = item.beer.beer_abv,
                        Ibu = item.beer.beer_ibu
                    });
                }
            }

            // var groups = objects.SelectMany(m => m.Styles).Select(m => m).Where(m => m != "-" && m != "/").Distinct();

            // var numberOfObjects = groups.GroupBy(m => new {group = m, number = objects.Count(o => o.Styles.Contains(m))}).Where(m => m.Key.number > 5).OrderByDescending(m => m.Key.number);

            var csvItems = objects.Where(m => m.Rating > 0).Select(m => new CsvBeer
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
