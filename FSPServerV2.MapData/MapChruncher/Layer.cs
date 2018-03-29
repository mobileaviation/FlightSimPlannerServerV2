﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            String bounds =  String.Format("{0:0.####},{1:0.####},{2:0.####},{3:0.####}", MapRectangle.NE.lon,
                    MapRectangle.NE.lat,
                    MapRectangle.SW.lon,
                    MapRectangle.SW.lat);
            return bounds;
        }

        public int GetMinZoom()
        {
            var zoom = from z in RangeDescriptors
                       where z.TilePresent
                       orderby z.Zoom 
                       group z by z.Zoom into zz
                       select zz.Min(z => z.Zoom);
            int rd = zoom.First();
            return zoom.First();
        }

        public int GetMaxZoom()
        {
            var zoom = from z in RangeDescriptors
                       where z.TilePresent
                       orderby z.Zoom 
                       group z by z.Zoom into zz
                       select zz.Max(z => z.Zoom);

            int rd = zoom.Last();
            return zoom.Last();
        }

        public String GetCenter()
        {
            return String.Format("{0:0.####},{1:0.####},{2}", MapRectangle.SW.lon - MapRectangle.NE.lon,
                MapRectangle.NE.lat - MapRectangle.SW.lat,
                GetMaxZoom() - GetMinZoom());
        }
    }
}
