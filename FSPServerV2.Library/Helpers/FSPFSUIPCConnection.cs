using FSPServerV2.Helpers;
using FSPServerV2.Models;
using FSUIPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library.Helpers
{
    public static class FSPFSUIPCConnection
    {
        public static void Open() { FSUIPCConnection.Open(); }
        public static void Close() { FSUIPCConnection.Close(); }
        public static bool IsOpen { get { return FSUIPCConnection.IsOpen; } }

        public static FsVersion FlightSimVersionConnected { get { return FSUIPCConnection.FlightSimVersionConnected; } }
        public static void DeleteGroup(String groupName) {
            if (_offsets != null)
            {
                _offsets.RemoveAll(offset => offset.Datagroup == groupName);
            }
            FSUIPCConnection.DeleteGroup(groupName);
        }

        private static List<FSPOffset> _offsets = new List<FSPOffset>();

        public static void AddOffset(FSPOffset offset)
        {
            if (_offsets == null) _offsets = new List<FSPOffset>();
            var checkOffset = from o in _offsets
                              where (o.Address == offset.Address) && (o.Datagroup == offset.Datagroup)
                              select o;
            if (checkOffset == null)
            {
                _offsets.Add(offset);
                return;
            }
            if (checkOffset.Count() == 0)
                _offsets.Add(offset);
        }

        public static void AddOffset(int Address, Datatype DataType, String group)
        {
            FSPOffset offset = OffsetHelpers.setOffset(Address, DataType.ToString(), group);
            AddOffset(offset);
        }

        public static List<FSPOffset> Process(String groupName)
        {
            FSUIPCConnection.Process(groupName);
            var offsets = from offset in _offsets
                          where offset.Datagroup == groupName
                          select offset;
            return offsets.ToList();
        }
    }
}
