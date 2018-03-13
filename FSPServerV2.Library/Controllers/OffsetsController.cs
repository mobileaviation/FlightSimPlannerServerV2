using FSPServerV2.Helpers;
using FSPServerV2.Models;
using FSUIPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;

namespace FSPServerV2.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class OffsetsController : ApiController
    {
        [HttpPost]
        [Route("offsets")]
        public HttpResponseMessage ReadOffsets([FromBody] List<OffsetRequest> offsets)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("Received OpenFSUIPC read offsets call");

            if (FSUIPCConnection.IsOpen)
            {
                if (offsets != null)
                {
                    if (offsets.Count() > 0)
                    {
                        try
                        {
                            string group = offsets.First().DataGroup;
                            FSUIPCConnection.DeleteGroup(group);

                            List<OffsetResponse> resp = new List<OffsetResponse>();
                            List<FSPOffset> _offsets = new List<FSPOffset>();
                            
                            foreach (OffsetRequest req in offsets)
                            {
                                _offsets.Add(OffsetHelpers.setOffset(req.Address, req.DataType.ToString(), group));
                            }

                            FSUIPCConnection.Process(group);

                            foreach (FSPOffset _offset in _offsets)
                            {
                                resp.Add(OffsetHelpers.setOffsetResponse(_offset));
                            }

                            //log.Debug("Response[0] {0}", resp[0].Value);
                            //log.Debug("Response[1] {0}", resp[1].Value);
                            //log.Debug("Response[2] {0}", resp[2].Value);
                            //log.Debug("Response[3] {0}", resp[3].Value);
                            //log.Debug("Response[4] {0}", resp[4].Value);

                            return Request.CreateResponse(HttpStatusCode.OK, resp);
                        }
                        catch (Exception ex)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception Gettig values",
                                ex);
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty offset list");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty or bad formatted offset list");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FSUIPC Connection is not opened!");
            }
        }

        [HttpGet]
        [Route("offset/{offset}")]
        public HttpResponseMessage ReadOffset(int offset, string datatype, string datagroup)
        {
            if (FSUIPCConnection.IsOpen)
            {
                FSUIPCConnection.DeleteGroup(datagroup);
                FSPOffset _offset = OffsetHelpers.setOffset(offset, datatype, datagroup);
                FSUIPCConnection.Process(datagroup);
                OffsetResponse resp = OffsetHelpers.setOffsetResponse(_offset);
                return Request.CreateResponse(HttpStatusCode.OK, resp);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FSUIPC Connection is closed!");
            }
        }
    }
}
