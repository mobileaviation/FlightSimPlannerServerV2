using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.FormApp
{
    public static class StartApp
    {
        public static void Start()
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("Starting FSUIPC WebAPI Console App");

            Int32 port = Properties.Settings.Default.Port;
            string baseAddress = "http://*:" + port.ToString() + "/";

            log.Info("FSUIPC Web API is listening on {0}", baseAddress);

            WebApp.Start<Startup>(url: baseAddress);

            log.Info("Web Api is successfully started");
        }
    }
}
