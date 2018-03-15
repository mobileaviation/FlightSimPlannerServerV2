using FSPServerV2.Helpers;
using FSPServerV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using FSPServerV2.Library.Helpers;

namespace FSPServerV2.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class OffsetsController : ApiController
    {
        [HttpPost]
        [Route("offsets")]
        public HttpResponseMessage AddOffsets([FromBody] List<OffsetRequest> offsets)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("Received FSUIPC add offsets call");

            if (FSPFSUIPCConnection.IsOpen)
            {
                if (offsets != null)
                {
                    if (offsets.Count() > 0)
                    {
                        try
                        {
                            string group = offsets.First().DataGroup;
                            
                            foreach (OffsetRequest req in offsets)
                            {
                                FSPFSUIPCConnection.AddOffset(req.Address, req.DataType, req.DataGroup);
                            }

                            HttpResponseMessage response = ReadOffsets(group);

                            return response;
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
        [Route("offsets")]
        public HttpResponseMessage ReadOffsets(string datagroup)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("Received OpenFSUIPC read offsets call");

            try
            {
                if (FSPFSUIPCConnection.IsOpen)
                {
                    List<FSPOffset> _offsets = FSPFSUIPCConnection.Process(datagroup);
                    List<OffsetResponse> resp = new List<OffsetResponse>();

                    foreach (FSPOffset _offset in _offsets)
                    {
                        resp.Add(OffsetHelpers.setOffsetResponse(_offset));
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FSUIPC Connection is not opened!");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception Getting Offsets", ex);
            }
        }

        [HttpGet]
        [Route("offset/{offset}")]
        public HttpResponseMessage ReadOffset(int offset, string datatype, string datagroup)
        {
            if (FSPFSUIPCConnection.IsOpen)
            {
                try
                {
                    List<FSPOffset> offsets = FSPFSUIPCConnection.Process(datagroup);
                    FSPOffset _offset = (from o in offsets
                                         where o.Address == offset
                                         select o).First();
                    OffsetResponse resp = OffsetHelpers.setOffsetResponse(_offset);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception Getting Offset", ex);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FSUIPC Connection is closed!");
            }
        }
    }
}
