using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library.Tests
{
    public static class StaticTest
    {
        private static List<String> strings;
        public static void Add(String s)
        {
            if (strings == null)
            {
                strings = new List<string>();
            }

            strings.Add(s);
        }

        public static List<String> GetStrings()
        {
            return strings;
        }
    }
}
