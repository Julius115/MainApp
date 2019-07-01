using System;
using System.Collections.Generic;
using System.Linq;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            View view = new View();

            List<string> anagrams = new List<string>();

            int minOutputSize = 4;
            int maxOutputSize = 10;

            List<string> anagramWords = new List<string>();

            //Console.WriteLine("Enter words");
            //String result = Console.ReadLine();
            List<string> input = new List<string>() { "sulaa", "lnkelis" };

            string tempInput = "sulaalnkelis";
            
            
            Dictionary<string, int> wordsList = view.ReadWordsFromFile("C:\\Users\\Julius\\Downloads\\zodynas.txt");

            List<string> keyList = new List<string>(wordsList.Keys);

            var containsLettersFromInput = keyList.Where(w => w.All(tempInput.Contains));

            foreach (string aa in containsLettersFromInput)
            {
                Console.WriteLine(aa);
            }

            foreach (string s in containsLettersFromInput)
            {
                string temp = tempInput.TrimEnd(s.ToArray<char>());
                Console.WriteLine(tempInput);
                Console.WriteLine(temp);
            }

            foreach (KeyValuePair<string, int> k in wordsList)
            {
                
                

                //Console.WriteLine(k.Key + " " + k.Value);
                /*foreach (string s in input)
                {
                    if (String.Concat(s.OrderBy(c => c)).Equals(String.Concat(k.Key.OrderBy(c => c))) && s.Length >= minOutputSize && anagrams.Count < maxOutputSize)
                    {
                        anagrams.Add(k.Key);
                    }
                    // TODO: get list of words containing characters
                }*/
            }

            foreach (string an in anagrams)
            {
                Console.WriteLine(an);
            }

            Console.ReadKey();
        }
    }
}
