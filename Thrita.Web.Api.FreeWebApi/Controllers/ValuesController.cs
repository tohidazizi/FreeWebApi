using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Thrita.Web.Api.FreeWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();

            string p1Val = allUrlKeyValues.SingleOrDefault(x => x.Key == "p1");
            string p2Val = allUrlKeyValues.SingleOrDefault(x => x.Key == "p2");
            string p3Val = allUrlKeyValues.SingleOrDefault(x => x.Key == "p3");
            
            return new string[] { "value1", "value2" , p1Val, p2Val, p3Val };
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
    }
}
