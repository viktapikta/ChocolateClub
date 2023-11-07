using ChocolateClub.Domain.Model;
using Dapper;

namespace ChocolateClub.Infrastructure
{
    public interface IChocolateRepository
    {
        Task AddChocolateBar(ChocolateBar chocolateBar);
        Task AddChocolateBarProperties(ChocolateBarProperties chocoProperties);
        Task<IEnumerable<ChocolateBar>> GetChocolateBars();
        Task<ChocolateCollection> GetChocolateCollection();
        Task<ChocolateBar> GetChocolateBar(int id);
        Task<ChocolateBarProperties> GetChocolateBarProperties(int barId);
        Task<IEnumerable<ChocolateBarProperties>> GetChocolateBarProperties();
        Task<IEnumerable<ChocolateBar>> GetChocolateBarsWithoutProperties();
        Task<IEnumerable<ChocolateBar>> SearchChocolateBars(string source);
        Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfiles();
    }

    public class ChocolateRepository : IChocolateRepository
    {
        private readonly INpgsqlConnectionFactory _connectionFactory;
        public ChocolateRepository(INpgsqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task AddChocolateBar(ChocolateBar chocolateBar)
        {

            string sql = "insert into chocolate.bar (id, name, maker, country, type, flavor, source, strain, more_info, rating, date_added) values(@Id, @Name, @Maker, @Country, @Type, @Flavor, @Source, @Strain, @MoreInfo, @Rating, @DateAdded)";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { Id = chocolateBar.Id, Name = chocolateBar.Name, Maker = chocolateBar.Maker, Country = chocolateBar.Country, Type = chocolateBar.Type, Flavor = chocolateBar.Flavor, Source = chocolateBar.Source, Strain = chocolateBar.Strain, MoreInfo = chocolateBar.MoreInfo, Rating = chocolateBar.Rating, DateAdded = DateTime.UtcNow });
            }
        }

        public async Task AddChocolateBarProperties(ChocolateBarProperties chocoProperties)
        {
            string sql = "insert into chocolate.properties (bar_id, appearance, aroma, mouth_feel, flavor, quality, date_added) values(@BarId, @Appearance, @Aroma, @MouthFeel, @Flavor, @Quality, @DateAdded)";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { BarId = chocoProperties.BarId, Appearance = chocoProperties.Appearance, Aroma = chocoProperties.Aroma, MouthFeel = chocoProperties.Mouthfeel, Flavor = chocoProperties.Flavor, Quality = chocoProperties.Quality, DateAdded = DateTime.UtcNow });
            }
        }

        public async Task<IEnumerable<ChocolateBar>> GetChocolateBars()
        {
            string sql = "select id, name, maker, country, type, flavor, source, strain, more_info as moreinfo, rating, date_added from chocolate.bar";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ChocolateBar>(sql);
        }

        public async Task<ChocolateCollection> GetChocolateCollection()
        {
            string sql = "select id, name, maker, country, type, flavor, source, strain, more_info as moreinfo, rating, date_added from chocolate.bar";
            using var conn = _connectionFactory.CreateConnection();
            var chocolates = new ChocolateCollection();
            chocolates.ChocolateBars = await conn.QueryAsync<ChocolateBar>(sql);
            return chocolates;
        }

        public async Task<IEnumerable<ChocolateBar>> SearchChocolateBars(string source)
        {
            string sql = "select id, name, maker, country, type, flavor, source, strain, more_info as moreinfo, rating, date_added from chocolate.bar where source = @source";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ChocolateBar>(sql, new { source });
        }

        public async Task<ChocolateBar> GetChocolateBar(int id)
        {
            string sql = "select id, name, maker, country, type, flavor, source, strain, more_info as moreinfo, rating, date_added from chocolate.bar where id = @id";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ChocolateBar>(sql, new { id });
        }

        public async Task<IEnumerable<ChocolateBar>> GetChocolateBarsWithoutProperties()
        {
            string sql = "select id, name, maker, country, type, flavor, source, strain, more_info as moreinfo, rating, date_added from chocolate.bar cb "+
                "where not exists (select 1 from chocolate.properties cp where cb.id = cp.bar_id)";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ChocolateBar>(sql);
        }

        public async Task<IEnumerable<ChocolateBarProperties>> GetChocolateBarProperties()
        {
            string sql = "select bar_id as BarId, appearance, aroma, mouth_feel as Mouthfeel, flavor, quality, date_added from chocolate.properties";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ChocolateBarProperties>(sql);
        }

        public async Task<ChocolateBarProperties> GetChocolateBarProperties(int id)
        {
            string sql = "select bar_id, appearance, aroma, mouth_feel, flavor, quality, date_added from chocolate.properties where bar_id = @id";
            using var conn = _connectionFactory.CreateConnection();
            var result = await conn.QueryFirstOrDefaultAsync<ChocolateBarProperties>(sql, new { id });
            return result;
        }

        public async Task<IEnumerable<ExtendedChocolateBarProfile>> GetExtendedChocolateBarProfiles()
        {
            string sql = "select cb.id, cb.name, cb.maker, cb.country, cb.type, cb.flavor, cb.source, cb.strain, cb.more_info as moreinfo, cb.rating, cp.appearance as AppearanceRating, cp.aroma as AromaRating, cp.mouth_feel as MouthfeelRating, cp.flavor as FlavorRating, cp.quality as QualityRating from chocolate.bar cb " +
                "LEFT JOIN chocolate.properties cp ON cb.id = cp.bar_id";
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ExtendedChocolateBarProfile>(sql);
        }
    }
}
