using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (String.IsNullOrEmpty(args[0]))
            {
                throw new System.ArgumentNullException();
            }

            var uri = new UriBuilder(args[0]).Uri;

            if (!Uri.IsWellFormedUriString(uri.ToString(), UriKind.Absolute))
            {
                throw new System.ArgumentException();
            }

            await Program.GetWebPage(uri.ToString());
        }

        public static async Task GetWebPage(String url)
        {
            using var httpClient = new HttpClient();
            String pageSource = "";

            try
            {
                pageSource = await httpClient.GetStringAsync(url);
            }
            catch (Exception error)
            {
                Console.WriteLine("Błąd wczasie pobierania strony");
                Console.WriteLine(error);
                return;
            }


            var emailRegex = @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                             + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

            var matchResult = Regex
                .Matches(pageSource, emailRegex)
                .Select(match => match.Value)
                .Distinct();

            foreach (var email in matchResult)
            {
                Console.WriteLine(email);
            }
        }
    }
}