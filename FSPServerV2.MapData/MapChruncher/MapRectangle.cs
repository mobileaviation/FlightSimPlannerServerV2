using System;
using System.Collections.Generic;
using System.Text;

namespace FSPServerV2.Maps.MapChruncher
{
    public class MapRectangle
    {
        public MapRectangle()
        {
            SW = new LatLon();
            NE = new LatLon();
        }

        public LatLon SW { get; set; }
        public LatLon NE { get; set; }
    }
}
