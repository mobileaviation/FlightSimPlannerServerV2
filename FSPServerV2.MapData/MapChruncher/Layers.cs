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

        }

        public List<Layer> LayerList { get; set; }
        public String basePath { get; set; }

        public Boolean LoadFromFile(String filename)
        {
            XDocument mapChruncherDoc = XDocument.Load(filename);
            basePath = Path.GetDirectoryName(filename);
            LayerList = LayersFactory.BuildLayers(mapChruncherDoc, basePath);

            return (LayerList == null) ? false : true;
        }
    }
}
