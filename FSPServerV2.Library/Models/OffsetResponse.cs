﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Models
{
    public class OffsetResponse
    {
        public int Address { get; set; }
        public Datatype DataType { get; set; }
        public string DataGroup { get; set; }
        public string Value { get; set; }
        //public string Version { get; set; }
    }
}
