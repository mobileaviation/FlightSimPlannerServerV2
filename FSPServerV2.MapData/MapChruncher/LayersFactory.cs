using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FSPServerV2.Maps.MapChruncher
{
    static public class LayersFactory
    {
        static public List<Layer>BuildLayers(XDocument mapChruncherDoc)
        {
            List<Layer> layers = new List<Layer>();

            var layerlist = mapChruncherDoc.Root.Descendants("LayerList");
            foreach(XElement l in layerlist.Descendants("Layer"))
            {
                Layer layer = new Layer();

                layer.DisplayName = l.Attribute("DisplayName").Value;
                layer.ReferenceName = l.Attribute("ReferenceName").Value;

                XElement tileNamingScheme = l.Element("TileNamingScheme");
                layer.TileNamingScheme.FilePath = tileNamingScheme.Attribute("FilePath").Value;
                layer.TileNamingScheme.FilePrefix = tileNamingScheme.Attribute("FilePrefix").Value;
                layer.TileNamingScheme.FileSuffix = tileNamingScheme.Attribute("FileSuffix").Value;
                layer.TileNamingScheme.Type = tileNamingScheme.Attribute("Type").Value;

                XElement mapRectangle = l.Element("SourceMapRecordList").Element("SourceMapRecord").Element("MapRectangle");
                var e = mapRectangle.Elements("LatLon");

                layer.MapRectangle.NE.lat = double.Parse(e.First().Attribute("lat").Value, System.Globalization.CultureInfo.InvariantCulture);
                layer.MapRectangle.NE.lon = double.Parse(e.First().Attribute("lon").Value, System.Globalization.CultureInfo.InvariantCulture);
                layer.MapRectangle.SW.lat = double.Parse(e.Last().Attribute("lat").Value, System.Globalization.CultureInfo.InvariantCulture);
                layer.MapRectangle.SW.lon = double.Parse(e.Last().Attribute("lon").Value, System.Globalization.CultureInfo.InvariantCulture);

                XElement thumbnail = l.Element("SourceMapRecordList").Element("SourceMapRecord");
                var thumbnails = from t in thumbnail.Elements("Thumbnail")
                                   select new Thumbnail
                                   {
                                       Width = Convert.ToInt32(t.Attribute("Width").Value),
                                       Height = Convert.ToInt32(t.Attribute("Height").Value),
                                       URL = t.Attribute("URL").Value
                                   };
                layer.Thumbnail200 = (from thumb in thumbnails
                                      where ((thumb.Height == 200 && thumb.Width<200) || (thumb.Height < 200 && thumb.Width == 200))
                                      select thumb).First();


                layer.Thumbnail500 = (from thumb in thumbnails
                                      where ((thumb.Height == 500 && thumb.Width < 500) || (thumb.Height < 500 && thumb.Width == 500))
                                      select thumb).First();

                var rangeDescriptors = l.Element("RangeDescriptors").Elements("RangeDescriptor");
                foreach(XElement descriptor in rangeDescriptors)
                {
                    RangeDescriptor rangeDescriptor = new RangeDescriptor();
                    rangeDescriptor.TileX = Convert.ToInt32(descriptor.Attribute("TileX").Value);
                    rangeDescriptor.TileY = Convert.ToInt32(descriptor.Attribute("TileY").Value);
                    rangeDescriptor.Zoom = Convert.ToInt32(descriptor.Attribute("Zoom").Value);
                    rangeDescriptor.QuadTreeLocation = Convert.ToInt64(descriptor.Attribute("QuadTreeLocation").Value);

                    layer.RangeDescriptors.Add(rangeDescriptor);
                }

                layers.Add(layer);
            }


            return layers;
        }
    }
}
