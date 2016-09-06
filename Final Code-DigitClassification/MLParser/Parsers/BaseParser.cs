using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MLParser.Interface;

namespace MLParser.Parsers
{
    public abstract class BaseParser : IRowParser
    {
        public abstract int ReadLabel(CsvReader reader);
        public abstract List<double> ReadData(CsvReader reader);

        //read the data
        protected List<double> ReadData(CsvReader reader, int startColumn, int? endColumn = null)
        {
            List<double> data = new List<double>();
            if (endColumn == null)
            {
                endColumn = reader.Parser.FieldCount;
            }
            for (int i = startColumn; i < endColumn; i++)
            {
                //read the value
                double value = Double.Parse(reader[i]);

                //store the normalized value
                data.Add(Normalize(value));
            }
            return data;
        }

        protected double Normalize(double value)
        {
            //values fall within 0-1
            return value / 255d;
        }
    } 
}