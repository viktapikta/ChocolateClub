using System.Globalization;

namespace ChocolateClub.Domain.Model
{
    public class ChocolateProfile
    {
        Dictionary<string, decimal> profile = new Dictionary<string, decimal>();
        public void Load(IEnumerable<string> properties)
        {
            foreach (var property in properties)
            {
                if (property.Contains("&nbsp;"))
                {
                    var propertyDescription = property.Split("&nbsp;");
                    var propertyName = propertyDescription[0].Trim();
                    if (propertyDescription[1].Contains("/"))
                    {
                        decimal parsedValue;
                        if (decimal.TryParse(propertyDescription[1].Trim().Split("/")[0], NumberStyles.AllowThousands | NumberStyles.Float, CultureInfo.InvariantCulture, out parsedValue))
                        {
                            var propertyValue = parsedValue;
                            profile.Add(propertyName, propertyValue);
                        }

                    }
                }
            }
        }

        public void Print()
        {
            foreach (var property in profile.Keys)
            {
                Console.WriteLine("{0} {1}", property, profile[property]);
            }
        }

        public ChocolateBarProperties ConvertToProperties()
        {
            var properties = new ChocolateBarProperties();
            if (profile != null)
            {
                decimal aroma, appearance, mouthFeel, quality, flavor;
                profile.TryGetValue("Aroma", out aroma);
                profile.TryGetValue("Appearance", out appearance);
                profile.TryGetValue("Mouthfeel", out mouthFeel);
                profile.TryGetValue("Quality", out quality);
                profile.TryGetValue("Flavor", out flavor);
                properties.Aroma = aroma;
                properties.Appearance = appearance;
                properties.Mouthfeel = mouthFeel;
                properties.Quality = quality;
                properties.Flavor = flavor;
            }

            return properties;
        }
    }
}
