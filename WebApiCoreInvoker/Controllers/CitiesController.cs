using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Net;

namespace WebApiCoreInvoker.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private const string cacheAppNameKey = "appname";
        //private const string url = "http://localhost:63540/api/cities/";
        private const string url = "http://citiesservice/api/cities/";

        public CitiesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return json;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public async Task<string> Get(string name)
        {
                  using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{url}{name}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        string appName = string.Empty;
                        if (memoryCache.TryGetValue(cacheAppNameKey, out appName))
                        {
                            return $"{appName} => {json}";
                        }
                        else
                        {
                            return json;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{name}")]
        public void Put(string name)
        {
            memoryCache.Set(cacheAppNameKey, name);
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            memoryCache.Remove(cacheAppNameKey);
        }
    }
}
