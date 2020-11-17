
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SusApi.Data.Interfaces;
using SusApi.Models;
using SusApi.Validations.SusFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SusController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISusRepository _susRepository;
        public SusController(ISusRepository susRepository, IMemoryCache memoryCache)
        {
            _susRepository = susRepository;
            _memoryCache = memoryCache;
        }

        public IActionResult Get(string modulo, string ufs, string anos, string meses = null, int? page = null, int? limit = null)
        {
            SusFilter filter = new SusFilter(modulo, anos?.Split(','), ufs?.Split(','), meses?.Split(','));
            var validation = new SusFilterValidation().Validate(filter);

            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(s => s.ErrorMessage));

            string cacheName = $"{modulo}-{ufs}-{anos}-{meses}-{page}-{limit}";
            bool exist = _memoryCache.TryGetValue(cacheName, out object cache);
            if (exist) return Ok(cache);

            var res = _susRepository.GetAll(modulo, ufs?.Split(','), anos?.Split(','), meses?.Split(','), page, limit);
            _memoryCache.Set(cacheName, res, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, 10) }); ;

            return Ok(res);
        }
    }
}
