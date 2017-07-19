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
    public class CitiesController : Controller
    {
        private const string filePath = "Cities.txt";

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var countries = System.IO.File.ReadAllLines(filePath);
            return countries;
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IEnumerable<string> Get(string name)
        {
            var countries = System.IO.File.ReadAllLines(filePath);
            var countriesFilter = countries.Where(c => c.ToLower().Contains(name.ToLower()));
            return countriesFilter;
        }

    }
}
