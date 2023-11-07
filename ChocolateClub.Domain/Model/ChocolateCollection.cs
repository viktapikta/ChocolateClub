namespace ChocolateClub.Domain.Model
{
    public class ChocolateCollection
    {
        public ChocolateCollection()
        {
            ChocolateBars = new List<ChocolateBar>();
        }

        public IEnumerable<ChocolateBar> ChocolateBars { get; set; }
    }
}
