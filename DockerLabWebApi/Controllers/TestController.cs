using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DockerLabWebApi.Controllers
{
    [RoutePrefix("api/tests")]
    public class TestController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<HttpResponseMessage> test()
        {
            string url = "http://192.168.28.126:9080/Api/OrdenesLiquidacion/ObtenerPlanillasProcesadasDeuda?periodo=201612&identificadorEmpleador=06141101210013";

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return Request.CreateResponse(HttpStatusCode.OK, json);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                }
            }
        }
    }
}
