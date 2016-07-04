using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowUI.Framework.Util
{
    public class StringContainsComparer : IComparer, IComparer<string>, IEqualityComparer<string>
    {
        public int Compare(object x, object y)
        {
            var expect = x as string;
            var actual = y as string;
            if (actual == null || expect == null) throw new InvalidOperationException();
            return (actual.Contains(expect)) ? 0 : -1;


        }

        public int Compare(string actual, string expect)
        {

            if (actual == null || expect == null) throw new InvalidOperationException();
            return (actual.Contains(expect)) ? 0 : -1;

        }

        public bool Equals(string actual, string expect)
        {
            if (actual == null || expect == null) throw new InvalidOperationException();
            return actual.Contains(expect);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
