using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputePI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Task<double> tas= ComputePi();
            Console.WriteLine(tas.Result);
            Console.ReadKey();
        }
        //1.Compute the following method in an async task and await the result:

            private static async Task<double> ComputePi()

            {

                var sum = 0.0;

                var step = 1e-9;
            
                await Task.Run(() =>
                {
                for (var i = 0; i < 1000000000; i++)

                {

                    var x = (i + 0.5) * step;

                    sum = sum + 4.0 / (1.0 + x * x);

                }
                }); 
                return sum * step;

            }
        
    }
}

