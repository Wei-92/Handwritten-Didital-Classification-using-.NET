using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLParser.Interface;
using MLParser.Types;
using CsvHelper;

namespace MLParser.Parsers
{
    //column 0 contains the label
    //remaining columns contain the data, i.e.,pixels
    public class ExistLabelParser:BaseParser
    {
        public override int ReadLabel(CsvReader reader)
        {
            return Int32.Parse(reader[0]);
        }

        public override List<double> ReadData(CsvReader reader)
        {
            //read the data from the first column
            return ReadData(reader, 1);
        }
    }
}
