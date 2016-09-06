using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.VectorMachines;
using System.IO;

namespace DigitClassification
{
    public static class Accuracy
    {
        public static double CalculateAccuracy(MulticlassSupportVectorMachine machine, double[][]inputs,int [] outputs)
        {
            double correct = 0;

            for (int i=0;i<inputs.Length;i++)
            {
                int output = machine.Compute(inputs[i]);
                if(output==outputs[i])
                {
                    correct++;
                }
            }
            return (correct / (double)inputs.Length);
        }

        public static int SaveOutput(MulticlassSupportVectorMachine machine, double[][] inputs, string path )
        {
            File.AppendAllText(path, "ImageID------Label\r\n");

            for (int i=0;i<inputs.Length;i++)
            {
                int output = machine.Compute(inputs[i]);
                File.AppendAllText(path, (i + 1) + "," + output.ToString() + "\r\n");
            }
            return inputs.Length;
        }
    }
}
