using System;

namespace api.Domain
{
    public class BeerStyleRatingModel
    {
        public Guid Id { get; protected set; }
        public string User { get; set; }
        public double Rating { get; set; }
        public string Ibu { get; set; }
        public string Abv { get; set; }
        public string Ipa { get; set; }
        public string Pale { get; set; }
        public string American { get; set; }
        public string Lager { get; set; }
        public string Imperial { get; set; }
        public string Stout { get; set; }
    }
}
