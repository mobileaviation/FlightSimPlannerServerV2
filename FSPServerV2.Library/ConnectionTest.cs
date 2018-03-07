using FSUIPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library
{
    public class ConnectionTest
    {
        public ConnectionTest()
        {
            isopen = FSUIPCConnection.IsOpen;
        }

        private Boolean isopen;
        public Boolean IsOpen { get { return isopen; } }

        public String status;
        public String Status { get { return status; } }

        public Boolean Connect()
        {
            try
            {
                if (!isopen)
                {
                    FSUIPCConnection.Open();
                    isopen = FSUIPCConnection.IsOpen;
                }

                status = "Connection opened to Simulator: " + FSUIPCConnection.FlightSimVersionConnected;
                return isopen;
            }
            catch(Exception e)
            {
                isopen = false;
                status = e.Message;
                return isopen;
            }
        }

        public Boolean Close()
        {
            try
            {
                if (FSUIPCConnection.IsOpen)
                {
                    FSUIPCConnection.Close();
                }

                status = "Connection is closed";
                isopen = FSUIPCConnection.IsOpen;
            }
            catch (Exception e)
            {
                isopen = false;
                status = e.Message;
            }

            return !isopen;
        }
    }
}
