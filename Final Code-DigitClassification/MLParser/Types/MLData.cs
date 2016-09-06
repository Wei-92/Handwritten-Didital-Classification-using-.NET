using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLParser.Types
{
    public class MLData
    {
        //Input
        public List<double> Data { get; set; }
        //Output
        public int Label { get; set; }
        public MLData()
        {
            Data = new List<double>();
        }
    }
}
