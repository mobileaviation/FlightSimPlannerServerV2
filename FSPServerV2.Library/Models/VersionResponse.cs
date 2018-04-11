using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library.Models
{
    public class VersionResponse
    {
        public VersionResponse(String version)
        {
            this.version = version.Split('.');
        }

        string[] version;

        public int Major { get { return Convert.ToInt32(version[0]); } }
        public int Minor { get { return Convert.ToInt32(version[1]); } }
        public int Revision { get { return Convert.ToInt32(version[2]); } }
        public int Build { get { return Convert.ToInt32(version[3]); } }
    }
}
