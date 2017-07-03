using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DockerLabWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return GetSystemDetails();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private List<string> GetSystemDetails()
        {
            var systemInfo = new List<string>();
            systemInfo.Add( Environment.UserName); // User name of PC
            //systemInfo.Add(getOSInfo(); // OS version of pc
            systemInfo.Add(Environment.MachineName);// Machine name
            string OStype = "";
            if (Environment.Is64BitOperatingSystem) { OStype = "64-Bit, "; } else { OStype = "32-Bit, "; }
            OStype += Environment.ProcessorCount.ToString() + " Processor";
            systemInfo.Add(OStype); // Processor type
            var toalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            double toal = Convert.ToDouble(toalRam / (1024 * 1024));
            int t = Convert.ToInt32(Math.Ceiling(toal / 1024).ToString());
            systemInfo.Add(t.ToString() + " GB");// ram detail

            return systemInfo;
        }
    }
}
