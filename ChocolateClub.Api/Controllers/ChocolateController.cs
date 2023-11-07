using ChocolateClub.Api.Services;
using ChocolateClub.Domain.Model;
using ChocolateClub.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateClub.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChocolateController : ControllerBase
    {
        private readonly ILogger<ChocolateController> _logger;
        private readonly IChocolateRepository _chocolateRepository;
        private readonly IChocolateService _chocolateService;

        public ChocolateController(ILogger<ChocolateController> logger, IChocolateRepository chocolateRepository, IChocolateService chocolateService)
        {
            _logger = logger;
            _chocolateRepository = chocolateRepository;
            _chocolateService = chocolateService;
        }

        [HttpGet]
        [Route("slow")]
        public IEnumerable<ExtendedChocolateBarProfile> Get()
        {
            return _chocolateService.GetExtendedChocolateBarProfileAsync1();
        }

        [HttpGet]
        [Route("stillslow")]
        public async Task<IEnumerable<ExtendedChocolateBarProfile>> StillSlow()
        {
            return await _chocolateService.GetExtendedChocolateBarProfileAsync2();
        }

        [HttpGet]
        [Route("maybefaster")]
        public IEnumerable<ExtendedChocolateBarProfile> MaybeFaster()
        {
            return _chocolateService.GetExtendedChocolateBarProfileAsync3();
        }

        [HttpGet]
        [Route("faster")]
        public async Task<IEnumerable<ExtendedChocolateBarProfile>> Faster()
        {
            return await _chocolateService.GetExtendedChocolateBarProfileAsync4();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<ChocolateBar>> GetAsync([FromQuery] string source)
        {
            var result = await _chocolateRepository.SearchChocolateBars(source);
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public ChocolateBar Get(int id)
        {
            var result = _chocolateRepository.GetChocolateBar(id).GetAwaiter().GetResult();
            return result;
        }
    }
}
