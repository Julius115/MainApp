using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainApp
{
    class Program
    {
        //static List<string> containsLettersFromInput = new List<string>() { "las", "sa" ,"s", "tal"};
        //static List<List<string>> anagrams = new List<List<string>>();
        static void Main(string[] args)
        {
            View view = new View();

            List<string> testInput = new List<string>() { "sula", "liaideta"};

            IWordRepository reader = new FileReader("C:\\Users\\Julius\\Downloads\\zodynas.txt");
            Dictionary<string, int> wordsList = reader.Load();

            IAnagramSolver solver = new AnagramSolverSingleWord(wordsList, testInput);
            List<string> anagrams = solver.SolveAnagrams();

            foreach (string s in anagrams)
            {
                Console.WriteLine(s);
            }


            #region temp

            /*
            //string test1 = "abcde";
            //string test2 = "be";
            //Console.WriteLine(test1.Contains(test2));

            //List<string> anagrams = new List<string>();

            int minOutputSize = 4;
            int maxOutputSize = 10;

            List<string> anagramWords = new List<string>();

            //Console.WriteLine("Enter words");
            //String result = Console.ReadLine();
            

            string tempInput = "salasatl";
            
            
            //Dictionary<string, int> wordsList = view.ReadWordsFromFile("C:\\Users\\Julius\\Downloads\\zodynas.txt");

            //List<string> keyList = new List<string>(wordsList.Keys);

            //List<string> containsLettersFromInput = keyList.Where(w => w.All(tempInput.Contains)).ToList();

            Dictionary<char, int> inputChars = new Dictionary<char, int>();
            
            for (int i = 0; i < tempInput.Length; i++)
            {
                if (!inputChars.ContainsKey(tempInput[i]))
                {
                    inputChars.Add(tempInput[i], 1);
                }
                else
                {
                    inputChars[tempInput[i]]++;
                }
            }

            

            foreach (var a in inputChars)
            {
                Console.WriteLine(a.Key + " " + a.Value);
            }

           

           


            for (int i = 0; i < containsLettersFromInput.Count; i++)
            {
                string tempWord = containsLettersFromInput[i];

                Dictionary<char, int> tempChars = new Dictionary<char, int>(inputChars);

                List<string> wordAnagrams = new List<string>();

                // removes with too mane leters
                foreach (char c in containsLettersFromInput[i])
                {
                    if (tempChars.ContainsKey(c) && tempChars[c] > 0)
                    {
                        tempChars[c]--;
                    }
                    else
                    {
                        containsLettersFromInput.Remove(tempWord);
                    }
                }
                

                int sum = tempChars.Sum(x => x.Value);

                Console.WriteLine(sum);
                //Dictionary<char, int> forILoopChars = inputChars;
                //for (int j = i +1; j < containsLettersFromInput.Count; j++)
                //{
                    
                //}

            }

            for (int i = 0; i < containsLettersFromInput.Count; i++)
            {
                Dictionary<char, int> tempChars2 = new Dictionary<char, int>(inputChars);
            //int cnt = 0;
                //foreach (string s in containsLettersFromInput)
                //{
                    RecAnagram(tempChars2, i, new List<string>());
                //cnt++;
                //}
            }

            Console.WriteLine("after recursion:");
            foreach (var l in anagrams)
            {
                Console.WriteLine("nauja: ");
                foreach (var s in l)
                {
                    Console.WriteLine("a: " + s);
                }
            }


            //foreach (string s in containsLettersFromInput)
            //{
            //    Console.WriteLine(s);
            //}

            //foreach (string aa in containsLettersFromInput)
            //{
            //  Console.WriteLine(containsLettersFromInput[1]);
            //}
            int count = 0;
            foreach (string aa in containsLettersFromInput)
            {
                count++;
                Console.WriteLine(aa);
            }
           
            Console.WriteLine(count);
             
            foreach (string s in containsLettersFromInput)
            {
                string temp = tempInput.TrimEnd(s.ToArray<char>());
                Console.WriteLine(tempInput);
                Console.WriteLine(temp);
            }
            
            foreach (KeyValuePair<string, int> k in wordsList)
            {
                
                

                //Console.WriteLine(k.Key + " " + k.Value);
                foreach (string s in input)
                {
                    if (String.Concat(s.OrderBy(c => c)).Equals(String.Concat(k.Key.OrderBy(c => c))) && s.Length >= minOutputSize && anagrams.Count < maxOutputSize)
                    {
                        anagrams.Add(k.Key);
                    }
                    // TODO: get list of words containing characters
                }
            //}

            /*foreach (string an in anagrams)
            {
                Console.WriteLine(an);
            }
            
            #endregion

            Console.ReadKey();
        }

        public static void RecAnagram(Dictionary<char, int> inputChars, int wordIndex, List<string> anagra)
        {
            //TODO: check wordIndex Out of bounds
            //if (wordIndex > 4)
            //{
            //    if (inputChars.Sum(x => x.Value) == 0 && anagra.Count > 0)
            //    {
            //        anagrams.Add(anagra);
            //    }
            //    return;
            //}

            bool contains = true;
            foreach (char c in containsLettersFromInput[wordIndex])
            {
                if (!inputChars.ContainsKey(c))
                {
                    //TODO: better check
                    contains = false;
                    break;
                }

            }

            if (contains = true)
            {
                foreach (char c in containsLettersFromInput[wordIndex])
                {

                    if (!inputChars.ContainsKey(c) || inputChars[c] < 1)
                    {
                        //if (anagra.Count > 0 /*&& inputChars.Sum(x => x.Value) == 0 )
                        //{
                        //    anagrams.Add(anagra);
                        //}
                        //return;
                        RecAnagram(inputChars, ++wordIndex, anagra);

                    }
                    else
                    {
                        inputChars[c]--;
                    }

                }
                //anagrams.Add(anagra);

                //int sum = inputChars.Sum(x => x.Value);
                //if (sum == 0)
                //{

                //}

                if (!anagra.Contains(containsLettersFromInput[wordIndex]))
                {
                    anagra.Add(containsLettersFromInput[wordIndex]);
                }
            }
            //anagrams.Add(anagra);

            if (wordIndex < 4)
            {
                RecAnagram(inputChars, ++wordIndex, anagra);
            }
        }
*/
            #endregion


        }

    }
}
