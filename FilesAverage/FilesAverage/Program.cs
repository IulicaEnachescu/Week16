using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FilesAverage
{
    class Program
    {
        /*- compute the average for numbers for each file
         * - compute the average for numbers in all files
- compute the sum for all numbers in all files*/
        static void Main(string[] args)
        {

            string[] files = Directory.GetFiles(@"C:\Users\Iulica\source\repos\Week16.1\FilesAverage\File");
            BlockingCollection<MathFile> mathFiles = new BlockingCollection<MathFile>();

            // Method signature: Parallel.ForEach(IEnumerable<TSource> source, Action<TSource> body)
            Parallel.ForEach(files, (currentFile) =>
            {
                var mathFile = new MathFile();
                mathFile.FileName = currentFile;
                StreamReader readFile = new StreamReader(currentFile);
                Console.WriteLine(mathFile.FileName);
                while (!readFile.EndOfStream)
                {
                    mathFile.Sum += int.Parse(readFile.ReadLine());
                    mathFile.Count++;
                }
                mathFile.Average = (double)mathFile.Sum / mathFile.Count;
                mathFiles.Add(mathFile);
            });

            // /*- compute the average for numbers for each file
            Console.WriteLine("Compute the average for numbers for each file");
            int sum = 0;
            int count = 0;
            foreach (var item in mathFiles)
            {
                Console.WriteLine($"File: {item.FileName}: Average: {(double)item.Sum / item.Count}");
                sum += item.Sum;
                count += item.Count;
            }
            Console.WriteLine("Compute the average for numbers for all file");
            Console.WriteLine($"Total average: {(double)sum / count}");
            //compute the sum for all numbers in all files
            Console.WriteLine($"Total sum: {sum}");
            Console.ReadKey();
            CalcSynchronous(files, mathFiles);

            Console.ReadKey();
        }

        private static void CalcSynchronous(string[] files, BlockingCollection<MathFile> mathFiles)
        {
            foreach (var currentFile in files)
            {

                var mathFile = new MathFile();
                mathFile.FileName = currentFile;
                Console.WriteLine(mathFile.FileName);
                StreamReader readFile = new StreamReader(currentFile);
                while (!readFile.EndOfStream)
                {
                    mathFile.Sum += int.Parse(readFile.ReadLine());
                    mathFile.Count++;
                }
                mathFile.Average = (double)mathFile.Sum / mathFile.Count;
                mathFiles.Add(mathFile);
            }

            // /*- compute the average for numbers for each file
            Console.WriteLine("Compute the average for numbers for each file");
            int sum = 0;
            int count = 0;
            foreach (var item in mathFiles)
            {
                Console.WriteLine($"File: {item.FileName}: Average: {(double)item.Sum / item.Count}");
                sum += item.Sum;
                count += item.Count;
            }
            Console.WriteLine("Compute the average for numbers for all file");
            Console.WriteLine($"Total average: {(double)sum / count}");
            //compute the sum for all numbers in all files
            Console.WriteLine($"Total sum: {sum}");
            Console.ReadKey();
        }
    }
    public class MathFile
{
        public string FileName { get; set; }
        public int Sum { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }
}
}
