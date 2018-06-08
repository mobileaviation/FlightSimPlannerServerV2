using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPServerV2.FormApp
{
    static class Program
    {
        //static Mutex mutex = new Mutex(false, "Flight Sim Planner V2 Server Mutex");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();

            //string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\Data";

            if (IsAdministrator())
            {
                form.serverRunning = StartApp.Start();
                form.startupMessage = "Server is " + ((form.serverRunning) ? "started" : "not started");
            }
            else
            {
                form.serverRunning = false;
                form.startupMessage = "The app has no Administrator priviledges, Please start 'as-Administrator'";
            }

            form.SetServerStatus();

            Application.Run(form);

        }

        static Boolean IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                .IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
