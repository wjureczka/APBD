using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = args[0];

            await Program.GetWebPage(url);
        }

        public static async Task GetWebPage(String url)
        {
            using var httpClient = new HttpClient();

            var pageSource = await httpClient.GetStringAsync(url);

            var emailRegex = @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                             + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            
            var mathResult = Regex.Matches(pageSource, emailRegex);

            foreach (var email in mathResult)
            {
                Console.WriteLine(email);
            }
        } 
    }
}