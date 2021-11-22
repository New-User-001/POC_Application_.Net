using System;
using System.Collections.Generic;

namespace ApplicationPOC
{
    public class POC
    {
        public string s3;
        public int summationData;
        public void stringAdd()
        {
            string s1 = "IIHT";
            string s2 = "Techademy";
            s3 = s1 + s2;
        }

        public void intSum()
        {
            int n1 = 20, n2 = 10;
            summationData = n1 + n2;
        }

        public long indexValue(List<long> data, int index)
        {
            return data[index];
        }

        public void Divide(int num1, int num2){
            var result = num1/num2;
        }

    }
}
