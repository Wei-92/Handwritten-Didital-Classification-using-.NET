using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLParser.Types;
using MLParser.Interface;
using CsvHelper;

namespace MLParser.Parsers
{
    //no label
    //for test
    public class NonLabelParser:BaseParser
    {
        public override int ReadLabel(CsvReader reader)
        {
            return 0;
        }

        public override List<double> ReadData(CsvReader reader)
        {
            return ReadData(reader, 0);
        }
    }
}
