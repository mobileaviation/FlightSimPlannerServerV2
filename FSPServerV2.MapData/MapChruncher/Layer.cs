using System;
using System.Collections.Generic;
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

        public void SetTileNamingScheme()
        {

        }
    }
}
