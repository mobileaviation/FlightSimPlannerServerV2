using FSPServerV2.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FSPServerV2.Library.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class ListFilesController : ApiController
    {
        public ListFilesController()
        {

        }

        [HttpGet]
        [Route("files")]
        public HttpResponseMessage Get(string filter)
        {
            List<FileResponse> files = null;
            try
            {
                string path = ConfigurationManager.AppSettings.Get("MBTilesPath");
                if (Directory.Exists(path))
                {
                    
                    var f = Directory.EnumerateFiles(path, filter).ToList();
                    var resp = from ff in f
                               select new FileResponse(ff);
                    files = resp.ToList();
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Directory: " + path + " not found!");
                }
            }
            catch(Exception ee)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FileList error: " + ee.Message);
            }

            if (files == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Files Found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
        }
    }
}
