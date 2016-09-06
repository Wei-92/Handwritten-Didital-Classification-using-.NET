using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace MLParser.Interface
{
    public interface IRowParser
    {
        //return labels (0,1,...,9); output
        int ReadLabel(CsvReader reader);

        //return data (784 columns); intput
        List<double> ReadData(CsvReader reader);
    }
}
