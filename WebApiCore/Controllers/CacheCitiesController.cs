using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using System.IO;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    public class CacheCitiesController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private const string filePath = "Cities.txt";
        private const string cacheKey = "cities";

        public CacheCitiesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ListCities();
        }


        // GET api/values/5
        [HttpGet("{name}")]
        public IEnumerable<string> Get(string name)
        {
            var countries = ListCities();
            var countriesFilter = countries.Where(c => c.ToLower().Contains(name.ToLower()));
            return countriesFilter;
        }

        private IEnumerable<string> ListCities()
        {
            string[] countries;
            if (!memoryCache.TryGetValue(cacheKey, out countries))
            {
                countries = System.IO.File.ReadAllLines(filePath);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(cacheKey, countries, cacheEntryOptions);
            }
            return countries;
        }
    }
}
