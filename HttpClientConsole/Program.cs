using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientConsole
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter word to get its anagrams:");
            string input = Console.ReadLine();
            Console.WriteLine();
            
            HttpResponseMessage response = await client.GetAsync("https://localhost:44323/home/GetAnagrams/" + input);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var objects = JArray.Parse(result);

            Console.WriteLine("Anagrams:");

            foreach (string item in objects)
            {
                Console.WriteLine(item);
            }
        }
    }
}
