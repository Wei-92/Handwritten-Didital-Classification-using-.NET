using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MLParser;
using MLParser.Interface;
using MLParser.Parsers;
using MLParser.Types;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using System.Configuration;

namespace DigitClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<<<<<< Training Data >>>>>>");
            var machine = RunSvm(_trainPath, _trainCount);

            Console.WriteLine("\n<<<<<< Cross Validation >>>>>>");
            RunSvm(_cvPath, _trainCount, machine);

            Console.WriteLine("\n<<<<<< Test Data >>>>>>");
            TestSvm(_testPath, "../../data/output.txt", _trainCount, machine);
            
        }

        private static MulticlassSupportVectorMachine RunSvm(string path, int count, MulticlassSupportVectorMachine machine=null)
        {
            double[][] inputs;
            int[] outputs;

            ReadData(path, count, out inputs, out outputs, new ExistLabelParser());

            //initial training
            if (machine ==null)
            {
                MulticlassSupportVectorLearning teacher = null;

                machine = new MulticlassSupportVectorMachine(_pixelCount, new Gaussian(_sigma), _classCount);
                
                //create the Multi-class learning algorithm for the machine
                teacher = new MulticlassSupportVectorLearning(machine, inputs, outputs);

                //configure the learning algorithm to use SMO 
                //to train the underlying SVMs in each of the binary class subproblems.
                teacher.Algorithm = (svm, classInputs, classOutputs, i, j) 
                    => new SequentialMinimalOptimization(svm, classInputs, classOutputs) 
                    { CacheSize = 0 };

                Utility.ShowProgressFor(() => teacher.Run(), "Training");
            }


            //calculate the accuracy
            double accuracy = Utility.ShowProgressFor<double>(() => Accuracy.CalculateAccuracy(machine, inputs, outputs), "Calculating Accuracy");
            Console.WriteLine("Accuracy: " + Math.Round(accuracy * 100, 2) + "%");

            return machine;

        }

        private static void TestSvm(string path, string outputPath, int count, MulticlassSupportVectorMachine machine)
        {
            double[][] inputs;
            int[] outputs;

            ReadData(path, count, out inputs, out outputs, new NonLabelParser());

            Utility.ShowProgressFor(() => Accuracy.SaveOutput(machine, inputs, outputPath), "Saving Output");
        }


        private static int ReadData(string path, int count, out double[][] inputs, out int [] outputs, IRowParser rowParser)
        {
            Parser parser = new Parser(rowParser);

            List<MLData> rows = Utility.ShowProgressFor<List<MLData>>(() => parser.Parse(path, count), "Reading Data");
            inputs = rows.Select(t => t.Data.ToArray()).ToArray();
            outputs = rows.Select(t => t.Label).ToArray();

            Console.WriteLine(rows.Count + "Rows have Processed.");

            return rows.Count;

        }

        //App.config Values
        private static int _pixelCount = Int32.Parse(ConfigurationManager.AppSettings["Width"]) * Int32.Parse(ConfigurationManager.AppSettings["Height"]);
        private static int _classCount = Int32.Parse(ConfigurationManager.AppSettings["ClassCount"]);
        private static int _trainCount = Int32.Parse(ConfigurationManager.AppSettings["TrainCount"]);
        private static double _sigma = Double.Parse(ConfigurationManager.AppSettings["Sigma"]);
        private static string _trainPath = ConfigurationManager.AppSettings["TrainPath"];
        private static string _cvPath = ConfigurationManager.AppSettings["CvPath"];
        private static string _testPath = ConfigurationManager.AppSettings["TestPath"];
    }
}
