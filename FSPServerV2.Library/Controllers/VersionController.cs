using FSPServerV2.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FSPServerV2.Library.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class VersionController : ApiController
    {
        public VersionController()
        {
           
        }

        [HttpGet]
        [Route("version")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new VersionResponse(Properties.Version.getVersion()));
        }
    }
}
