using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Save in "output.txt" file a the count of distinct words for each id.
namespace ReadDataFromFiles
{
    class Program
    {

        //Programul nu functioneaza deoarece nu introduce datele in dictionar
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"C:\Users\Iulica\source\repos\Week16.1\ReadDataFromFiles\Files");
            BlockingCollection<FileRead> mathFiles = new BlockingCollection<FileRead>();
            FileRead[] filesReads = new FileRead[2];
            int i = 0;


            // Read files to dictionary asynchronous
            Parallel.ForEach(files, (currentFile) =>
            {

            var fileRead = new FileRead();
            fileRead.FileName = currentFile;
            Console.WriteLine(fileRead.FileName);
                
            var idDictionary = new Dictionary<int, List<string>>();
            StreamReader readFile = new StreamReader(currentFile);
               
                while (!readFile.EndOfStream)
                {
                    string line= readFile.ReadLine();
                    //Console.WriteLine(line);
                    string[] split = line.Split(' ');


                    if (idDictionary.ContainsKey(int.Parse(split[0])))
                    {

                        idDictionary[int.Parse(split[0])].Add(split[1]);

                    }

                    else
                    {
                        idDictionary[int.Parse(split[0])] = new List<string>()
                        { split[1]
                        };


                    }
                   
                    fileRead.StringDictionary = idDictionary;
                    mathFiles.Add(fileRead);
                }
            });

            foreach (var item in mathFiles)
            {
                filesReads[i] = item;
            }
            //synchronous concat dictionaries into dictionary 1

            var dict1 = filesReads[0].StringDictionary;
           
            var dict2 = filesReads[1].StringDictionary;
            Console.WriteLine(filesReads[1].FileName);

            foreach (var key2 in dict2)
            {
                // If the dictionary already contains the key then merge them
                if (dict1.ContainsKey(key2.Key))
                {
                    dict1[key2.Key].AddRange(key2.Value);
                    continue;
                }
                dict1.Add(key2.Key, key2.Value);
            }
            //synchronous new dictionary count distinct strings in dictionary
            var dict = new Dictionary<int, int>();
            foreach (var item in dict1)
            {
                dict.Add(item.Key, item.Value.Distinct().Count());
            }
        }
        //


        static void Write(Dictionary<int, int> dictionary)
        {

            using (StreamWriter file = new StreamWriter(@"C:\Users\Iulica\source\repos\Week16.1\output.txt"))
            {
                foreach (var entry in dictionary)
                {
                    file.WriteLine("[{0}  {1}]", entry.Key, entry.Value);
                    Console.WriteLine("[{0}  {1}]", entry.Key, entry.Value);
                }
            }

        }
        public class FileRead
        {
            public Dictionary<int, List<string>> StringDictionary { get; set; }
            public string FileName { get; set; }
        }
    }
}


