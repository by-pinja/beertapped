using System;

namespace api.Domain
{
    public class BeerStyleRatingModel
    {
        public Guid Id { get; protected set; }
        public string User { get; set; }
        public double Rating { get; set; }
        public int Ibu { get; set; }
        public double Abv { get; set; }
        public int Ipa { get; set; }
        public int Pale { get; set; }
        public int American { get; set; }
        public int Lager { get; set; }
        public int Imperial { get; set; }
        public int Stout { get; set; }
        public int Ale { get; set; }
    }
}
