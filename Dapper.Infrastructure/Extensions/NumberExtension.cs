using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Extensions
{
    public static class NumberExtension
    {
        public static bool IsContain(this List<int> root, List<int> valueCompare)
        {
            foreach (var value in valueCompare)
            {
                if (root.Contains(value) == false)
                    return false;
            }

            return true;
        }

        public static bool IsContainAnyElement(this List<int> root, List<int> valueCompare)
        {
            foreach (var value in valueCompare)
            {
                if (root.Contains(value) )
                    return true;
            }

            return false;
        }

        public static bool IsEqual(this List<int> value1, List<int> value2)
        {
            if (value1.Count != value2.Count) return false;
            foreach (var value in value1)
            {
                if (value2.Contains(value) == false)
                    return false;
            }

            return true;
        }
    }
}
