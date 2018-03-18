using FSPServerV2.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using FSPServerV2.Helpers;
using NLog;
using FSPServerV2.Library.Helpers;

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
            String version = FSPServerV2.Library.Properties.Version.getVersion();


            String resp = "Server Version: " + version + " Sim Connected : ";
            if (FSPFSUIPCConnection.IsOpen)
            {
                return resp + FSPFSUIPCConnection.FlightSimVersionConnected;;
            }
            else
            {
                return resp + "Not Connected!";
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
           
                if (FSPFSUIPCConnection.IsOpen)
                {
                    log.Info("Connection already is open");
                    resp.Message = "Connection Opened";
                }
                else
                {
                    log.Info("Opening connection..");

                    FSPFSUIPCConnection.Open();
                    resp.Message = "Connection Opened";
                }

                FSPFSUIPCConnection.AddOffset(15616, Datatype.String, "Connect");
                FSPOffset _offset = FSPFSUIPCConnection.Process("Connect").First();
                OffsetResponse _resp = OffsetHelpers.setOffsetResponse(_offset);

                resp.Version = FSPServerV2.Library.Properties.Version.getVersion();

                resp.Simulator = FSPFSUIPCConnection.FlightSimVersionConnected.ToString();
                resp.Aircraft = _resp.Value;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Connection Error", ee);
                //return Request.CreateResponse(HttpStatusCode.OK, "error: " + ee.Message);
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
                FSPFSUIPCConnection.Close();
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
