using System;
using System.Collections.Generic;
using System.Linq;
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
        public String Get()
        {
            return Properties.Version.getVersion();
        }
    }
}
