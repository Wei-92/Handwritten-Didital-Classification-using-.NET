using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitClassification
{
    public static class Utility
    {

        public static T ShowProgressFor<T>(Func<T> action, string message)
        {
            T result;

            Console.Write(message + ">>>>>>>>>>>>>>");
            result = action();
            Console.WriteLine("Successfully Done!\n");

            return result;
        }
    }
}
