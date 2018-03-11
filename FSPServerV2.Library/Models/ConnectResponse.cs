using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Models
{
    public class ConnectResponse
    {
        public string Message { get; set; }
        public string Simulator { get; set; }
        public string Aircraft { get; set; }
        public string Version { get; set; }
    }
}
