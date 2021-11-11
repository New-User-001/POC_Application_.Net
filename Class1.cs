using System;
using System.Collections.Generic;

namespace ApplicationPOC
{
    public class POC
    {
        public string stringAdd(string s1,string s2)
        {
            return s1 + s2;
        }

        public long longCount(List<long> data)
        {
            return data.Count;
        }

        public long indexValue(List<long> data, int index)
        {
            return data[index];
        }
    }
}
