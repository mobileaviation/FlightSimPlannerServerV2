using FSPServerV2.Library.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FSPServerV2.Library.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class TestController : ApiController
    {
        public TestController()
        {

        }

        [Route("testadd")]
        [HttpGet] 
        public String GetTestAdd(String test)
        {
            StaticTest.Add(test);

            return "Added:" + test;
        }

        [Route("testget")]
        [HttpGet]
        public List<String> GetTest()
        {
            return StaticTest.GetStrings();
        }
    }
}
