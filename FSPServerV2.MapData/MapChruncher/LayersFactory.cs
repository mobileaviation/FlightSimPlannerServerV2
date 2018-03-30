using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FSPServerV2.Maps.MapChruncher
{
    static public class LayersFactory
    {
        static public List<Layer> BuildLayers(XDocument mapChruncherDoc, String basePath)
        {
            List<Layer> layers = new List<Layer>();

            var version = mapChruncherDoc.Root.Descendants("MapCruncherAppVersion");
            if (version.Count() > 0)
            {
                String v = version.First().Attribute("version").Value;
                if (!v.Equals("3.2.4")) return null;
            }
            else return null;
            
            var layerlist = mapChruncherDoc.Root.Descendants("LayerList");
            foreach (XElement l in layerlist.Descendants("Layer"))
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

                double lat1 = double.Parse(e.First().Attribute("lat").Value, System.Globalization.CultureInfo.InvariantCulture);
                double lon1 = double.Parse(e.First().Attribute("lon").Value, System.Globalization.CultureInfo.InvariantCulture);
                double lat2 = double.Parse(e.Last().Attribute("lat").Value, System.Globalization.CultureInfo.InvariantCulture);
                double lon2 = double.Parse(e.Last().Attribute("lon").Value, System.Globalization.CultureInfo.InvariantCulture);

                layer.MapRectangle.NE.lat = (lat1 < lat2) ? lat2 : lat1;
                layer.MapRectangle.NE.lon = (lon1 < lon2) ? lon2 : lon1;
                layer.MapRectangle.SW.lat = (lat1 > lat2) ? lat2 : lat1;
                layer.MapRectangle.SW.lon = (lon1 > lon2) ? lon2 : lon1;

                XElement thumbnail = l.Element("SourceMapRecordList").Element("SourceMapRecord");
                var thumbnails = from t in thumbnail.Elements("Thumbnail")
                                 select new Thumbnail
                                 {
                                     Width = Convert.ToInt32(t.Attribute("Width").Value),
                                     Height = Convert.ToInt32(t.Attribute("Height").Value),
                                     URL = t.Attribute("URL").Value
                                 };
                layer.Thumbnail200 = (from thumb in thumbnails
                                      where ((thumb.Height == 200 && thumb.Width < 200) || (thumb.Height < 200 && thumb.Width == 200))
                                      select thumb).First();


                layer.Thumbnail500 = (from thumb in thumbnails
                                      where ((thumb.Height == 500 && thumb.Width < 500) || (thumb.Height < 500 && thumb.Width == 500))
                                      select thumb).First();

                var rangeDescriptors = l.Element("RangeDescriptors").Elements("RangeDescriptor");
                String path = basePath + @"\" + layer.TileNamingScheme.FilePrefix + @"\";
                String ext = layer.TileNamingScheme.FileSuffix;

                // There is a problem (bug) with the RangeDescriptors from the metadata xml. There are only max 100 entries in 
                // this xml. Thats why we have to parse all the .png files so we add all the tiles available and not clip this to 100 
                // tiles max

                var files = Directory.EnumerateFiles(path, "*" + ext);
                foreach(String file in files)
                {
                    String key = Path.GetFileNameWithoutExtension(file);
                    RangeDescriptor rangeDescriptor = new RangeDescriptor();
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    QuadKeyToTileXY(key, out x, out y, out z);
                    double yy = Math.Pow(2, z) - 1;
                    rangeDescriptor.TileX = x;
                    rangeDescriptor.TileY = (int)Math.Round(yy) - y;
                    rangeDescriptor.Zoom = z;
                    rangeDescriptor.QuadTreeLocation = Convert.ToInt64(key);
                    layer.RangeDescriptors.Add(rangeDescriptor);
                }

                layers.Add(layer);
            }


            return layers;
        }

        public static void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int levelOfDetail)
        {
            tileX = tileY = 0;
            levelOfDetail = quadKey.Length;
            for (int i = levelOfDetail; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (quadKey[levelOfDetail - i])
                {
                    case '0':
                        break;

                    case '1':
                        tileX |= mask;
                        break;

                    case '2':
                        tileY |= mask;
                        break;

                    case '3':
                        tileX |= mask;
                        tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }
        }
    }
}
