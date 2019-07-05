using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientConsole
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {

            HttpResponseMessage response = await client.GetAsync("https://localhost:44323/home/GetAnagrams/sula");
            //HttpResponseMessage response = await client.GetAsync("https://localhost:44323/home/Index/sula");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("asd");
            Console.WriteLine(responseBody);

        }
    }
}
