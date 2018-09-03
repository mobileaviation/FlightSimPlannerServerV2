using System;
using System.Collections.Generic;
using System.Text;

namespace FSPServerV2.Maps.MapChruncher
{
    public class RangeDescriptor
    {
        public RangeDescriptor()
        {
            TilePresent = false;
        }

        public int TileX { get; set; }
        public int TileY { get; set; }
        public int Zoom { get; set; }
        public String QuadTreeLocation { get; set; }
        public Boolean TilePresent { get; set; }
    }
}
