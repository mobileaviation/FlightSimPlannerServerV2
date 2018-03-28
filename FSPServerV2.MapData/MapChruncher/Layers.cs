using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FSPServerV2.Maps.MapChruncher
{
    public class Layers
    {
        public Layers()
        {
            LayerList = new List<Layer>();
        }

        public List<Layer> LayerList { get; set; }
        public String basePath { get; set; }

        public void LoadFromFile(String filename)
        {
            XDocument mapChruncherDoc = XDocument.Load(filename);
            List<Layer> layers = LayersFactory.BuildLayers(mapChruncherDoc);
            basePath = Path.GetDirectoryName(filename);
        }
    }
}
