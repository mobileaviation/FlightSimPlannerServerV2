using FSPServerV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FSUIPC;
using System.Net.Http;
using System.Net;
using FSPServerV2.Helpers;
using NLog;

namespace FSPServerV2.Controllers
{
    [RoutePrefix("v1/fsuipc")]
    public class ConnectController : ApiController
    {
        /// <summary>
        /// Get the status of the FSUIPC Connection
        /// </summary>
        /// <returns>String with the connected simulator type or "Not Connected"</returns>
        [Route("status")]
        [HttpGet]
        public string Get()
        {
            if (FSUIPCConnection.IsOpen)
            {
                return FSUIPCConnection.FlightSimVersionConnected.ToString();
            }
            else
            {
                return "Not Connected!";
            }
        }

        /// <summary>
        /// Post to open the FSUIPC connection to the simulator 
        /// </summary>
        /// <param name="value">FSUIPCConnect type, supply the name of the application</param>
        /// <returns>Connection Status</returns>
        [Route("open")]
        [HttpPost]
        public HttpResponseMessage OpenPost([FromBody]FSUIPCConnect value)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            try
            {
                log.Info("Received OpenFSUIPC Connection call");
                ConnectResponse resp = new ConnectResponse();

                if (FSUIPCConnection.IsOpen)
                {
                    log.Info("Connection already is open");
                    resp.Message = "Connection is open";
                }
                else
                {
                    log.Info("Opening connection..");

                    FSUIPCConnection.Open();
                    resp.Message = "Connection Opened";
                }

                FSPOffset _offset = OffsetHelpers.setOffset(15616, "String", "Connect");
                FSUIPCConnection.Process("Connect");
                OffsetResponse _resp = OffsetHelpers.setOffsetResponse(_offset);

                resp.Simulator = FSUIPCConnection.FlightSimVersionConnected.ToString();
                resp.Aircraft = _resp.Value;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Connection Error", ee);
            }
        }

        /// <summary>
        /// Post to close the FSUIPC connection to the simulator 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Status</returns>
        [Route("close")]
        [HttpPost]
        public HttpResponseMessage ClosePost([FromBody]FSUIPCConnect value)
        {
            try
            {
                FSUIPCConnection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Connection Closed");
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error closing connection",
                    ee);
            }
        }
    }
}
