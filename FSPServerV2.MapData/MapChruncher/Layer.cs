using System;
using System.Collections.Generic;
using System.Linq;

namespace FSPServerV2.Maps.MapChruncher
{
    public class Layer
    {
        public Layer()
        {
            RangeDescriptors = new List<RangeDescriptor>();
            TileNamingScheme = new TileNamingScheme();
            MapRectangle = new MapRectangle();
            Thumbnail200 = new Thumbnail();
            Thumbnail500 = new Thumbnail();
        }


        public String DisplayName { get; set; }
        public String ReferenceName { get; set; }
        public TileNamingScheme TileNamingScheme { get; set; }
        public MapRectangle MapRectangle { get; set; }
        public Thumbnail Thumbnail200 { get; set; }
        public Thumbnail Thumbnail500 { get; set; }
        public List<RangeDescriptor> RangeDescriptors { get; set; }

        public String GetBounds()
        {
            String bounds =  String.Format("{0},{1},{2},{3}", 
                MapRectangle.SW.lon.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture),
                    MapRectangle.SW.lat.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture),
                    MapRectangle.NE.lon.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture),
                    MapRectangle.NE.lat.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture));
            return bounds;
        }

        public int GetMinZoom()
        {
            try
            {
                var zoom = from z in RangeDescriptors
                           where z.TilePresent
                           orderby z.Zoom
                           group z by z.Zoom into zz
                           select zz.Min(z => z.Zoom);
                int rd = zoom.First();
                return zoom.First();
            }
            catch (Exception ee)
            {
                return -1;
            }
        }

        public int GetMaxZoom()
        {
            try
            {
                var zoom = from z in RangeDescriptors
                           where z.TilePresent
                           orderby z.Zoom
                           group z by z.Zoom into zz
                           select zz.Max(z => z.Zoom);

                int rd = zoom.Last();
                return zoom.Last();
            }
            catch(Exception ee)
            {
                return -1;
            }
        }

        public String GetCenter()
        {
            int z = GetMinZoom() + ((GetMaxZoom() - GetMinZoom()) / 2);
            double lon = MapRectangle.SW.lon + ((MapRectangle.NE.lon - MapRectangle.SW.lon) / 2);
            double lat = MapRectangle.SW.lat + ((MapRectangle.NE.lat - MapRectangle.SW.lat) / 2);

            return String.Format("{0},{1},{2}", 
                lon.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture), 
                lat.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture), 
                z);
        }
    }
}
