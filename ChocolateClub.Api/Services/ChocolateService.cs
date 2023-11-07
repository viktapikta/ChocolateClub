using ChocolateClub.Domain.Model;
using ChocolateClub.Infrastructure;

namespace ChocolateClub.Api.Services
{
    public interface IChocolateService
    {
        IEnumerable<ExtendedChocolateBarProfile> GetExtendedChocolateBarProfileAsync1();
        Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfileAsync2();
        IEnumerable<ExtendedChocolateBarProfile> GetExtendedChocolateBarProfileAsync3();
        Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfileAsync4();
    }

    public class ChocolateService : IChocolateService
    {
        private readonly IChocolateRepository _chocolateRepository;
        private readonly IChocolateDescriptionProviderService _chocolateDescriptionProviderService;
        public ChocolateService(IChocolateRepository chocolateRepository, IChocolateDescriptionProviderService chocolateDescriptionProviderService)
        {
            _chocolateRepository = chocolateRepository ?? throw new ArgumentNullException(nameof(chocolateRepository));
            _chocolateDescriptionProviderService = chocolateDescriptionProviderService ?? throw new ArgumentNullException(nameof(chocolateDescriptionProviderService));
        }

        public IEnumerable<ExtendedChocolateBarProfile> GetExtendedChocolateBarProfileAsync1()
        {
            var result = new List<ExtendedChocolateBarProfile>();
            var allBars = _chocolateRepository.GetChocolateBars().GetAwaiter().GetResult();
            var allBarProperties = _chocolateRepository.GetChocolateBarProperties().GetAwaiter().GetResult();
            foreach(var bar in allBars)
            {
                var extendedChocolateBarProfile = new ExtendedChocolateBarProfile
                {
                    Id = bar.Id,
                    Country = bar.Country,
                    Source = bar.Source,
                    Flavor = bar.Flavor,
                    Maker = bar.Maker,
                    Name = bar.Name,
                    Rating = bar.Rating,
                    Strain = bar.Strain,
                    Type = bar.Type,
                    AppearanceRating = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id)?.Appearance,
                    AromaRating = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id)?.Aroma,
                    FlavorRating = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id)?.Flavor,
                    MouthfeelRating = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id)?.Mouthfeel,
                    QualityRating = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id)?.Quality,
                    Description = _chocolateDescriptionProviderService.GetChocolateBarDescription(bar.Id)
                };
                result.Add(extendedChocolateBarProfile);
            }

            return result;
        }

        public async Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfileAsync2()
        {
            var result = new List<ExtendedChocolateBarProfile>();
            var allBars = await _chocolateRepository.GetChocolateBars();
            var allBarProperties = await _chocolateRepository.GetChocolateBarProperties();
            foreach (var bar in allBars)
            {
                var barProperties = allBarProperties.FirstOrDefault(b => b.BarId == bar.Id);
                var extendedChocolateBarProfile = new ExtendedChocolateBarProfile
                {
                    Id = bar.Id,
                    Country = bar.Country,
                    Source = bar.Source,
                    Flavor = bar.Flavor,
                    Maker = bar.Maker,
                    Name = bar.Name,
                    Rating = bar.Rating,
                    Strain = bar.Strain,
                    Type = bar.Type,
                    AppearanceRating = barProperties?.Appearance,
                    AromaRating = barProperties?.Aroma,
                    FlavorRating = barProperties?.Flavor,
                    MouthfeelRating = barProperties?.Mouthfeel,
                    QualityRating = barProperties?.Quality,
                };
                result.Add(extendedChocolateBarProfile);
            }

            return result;
        }

        public IEnumerable<ExtendedChocolateBarProfile> GetExtendedChocolateBarProfileAsync3()
        {
            return _chocolateRepository.GetExtendedChocolateBarProfiles().GetAwaiter().GetResult();
        }

        public async Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfileAsync4()
        {
            return await _chocolateRepository.GetExtendedChocolateBarProfiles();
        }
    }
}
