namespace ChocolateClub.Domain.Model
{
    public class ExtendedChocolateBarProfile
    {
        public int Id { get; set; }
        public string Rating { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string Flavor { get; set; }
        public string Source { get; set; }
        public string Strain { get; set; }
        public decimal? AppearanceRating { get; set; }
        public decimal? AromaRating { get; set; }
        public decimal? MouthfeelRating { get; set; }
        public decimal? FlavorRating { get; set; }
        public decimal? QualityRating { get; set; }
        public string Description { get; set; }
    }
}
