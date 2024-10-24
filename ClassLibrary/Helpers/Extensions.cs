using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public static class Extensions
    {
        public static int InsertElementAscending(this List<int> source,
                int element)
        {
            int index = source.FindLastIndex(e => e < element);
            if (index == -1)
            {
                source.Insert(0, element);
                return element;
            }
            source.Insert(index + 1, element);
            return element;
        }
    }
}
