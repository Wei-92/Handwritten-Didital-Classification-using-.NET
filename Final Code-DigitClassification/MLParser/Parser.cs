using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MLParser.Interface;
using MLParser.Types;
using System.IO;

namespace MLParser
{
    public class Parser
    {
        static void Main(string[] args)
        {
        }

        private IRowParser _rowParser = null;

        public Parser(IRowParser rowParser)
        {
            _rowParser = rowParser;
        }

        public List <MLData> Parse(string path, int maxRows=0)
        {
            List<MLData> dataList = new List<MLData>();

            using (FileStream f=new FileStream(path,FileMode.Open))
            {
                using (StreamReader streamReader=new StreamReader(f, Encoding.GetEncoding(1252)))
                {
                    using (CsvReader csvReader =new CsvReader (streamReader))
                    {
                        csvReader.Configuration.HasHeaderRecord = false;

                        while (csvReader.Read())
                        {
                            MLData row = new MLData()
                            {
                                Label = _rowParser.ReadLabel(csvReader),
                                Data = _rowParser.ReadData(csvReader)
                            };

                            dataList.Add(row);

                            if (maxRows > 0 && dataList.Count >= maxRows)
                                break;
                        }
                    }
                }
            }
            return dataList;
        }
    }
}