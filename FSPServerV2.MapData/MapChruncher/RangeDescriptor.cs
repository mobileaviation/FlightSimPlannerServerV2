using System;
using System.Collections.Generic;
using System.Text;

namespace FSPServerV2.Maps.MapChruncher
{
    public class RangeDescriptor
    {
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int Zoom { get; set; }
        public long QuadTreeLocation { get; set; }
    }
}
