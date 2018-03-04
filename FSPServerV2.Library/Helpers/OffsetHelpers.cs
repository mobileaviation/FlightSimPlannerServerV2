using FSPServerV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Helpers
{
    public static class OffsetHelpers
    {
        public static FSPOffset setOffset(int offset, string datatype, string datagroup)
        {
            Datatype d = DatatypeHelper.ParseDatatype(datatype);
            return new FSPOffset(offset, d, datagroup);
        }

        public static OffsetResponse setOffsetResponse(FSPOffset offset)
        {
            OffsetResponse resp = new OffsetResponse();
            resp.Address = offset.Address;
            resp.DataGroup = offset.Datagroup;
            resp.DataType = offset.DataType;
            resp.Value = offset.Value;
            return resp;
        }
    }
}
