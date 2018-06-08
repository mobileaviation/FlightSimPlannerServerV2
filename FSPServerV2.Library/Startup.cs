﻿using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace FSPServerV2
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/fsuipc/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );

            string path = "";

            path = ConfigurationManager.AppSettings.Get("MBTilesPath");
            if (path == null)
            {
                var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                path =  Path.GetDirectoryName(conf.FilePath) + @"\maps";
                var settings = conf.AppSettings.Settings;
                settings.Add("MBTilesPath", path);
                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(conf.AppSettings.SectionInformation.Name);
            }

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);


            var physicalFileSystem = new PhysicalFileSystem(path);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "SamplePage.html" };
            options.EnableDirectoryBrowsing = false;

            appBuilder.UseWebApi(config);
            appBuilder.UseFileServer(options);
        }
    }
}
