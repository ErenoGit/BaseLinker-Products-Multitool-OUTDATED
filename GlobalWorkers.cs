using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLinker_Products_Multitool
{
    class GlobalWorkers
    {
        public const string BLApi = "https://api.baselinker.com/connector.php";
        public static string GetTokenAPI()
        {
            Console.WriteLine(Resources.Language.EnterAPIToken);
            return Console.ReadLine();
        }

        public static string GetCategory()
        {
            Console.WriteLine();
            Console.WriteLine(Resources.Language.EnterCategory);
            return Console.ReadLine();
        }

        public static string GetCheckingKey()
        {
            char checkingKey;

            Console.WriteLine();
            Console.WriteLine(Resources.Language.HowToCheckDuplicates);
            Console.WriteLine("1. " + Resources.Language.HowToCheckDuplicates1 + " (sku)");
            Console.WriteLine("2. " + Resources.Language.HowToCheckDuplicates2 + " (ean)");
            Console.WriteLine("3. " + Resources.Language.HowToCheckDuplicates3 + " (name)");
            
            while (true)
            {
                checkingKey = Console.ReadKey(true).KeyChar;

                if(checkingKey == '1')
                    return "sku";
                else if (checkingKey == '2')
                    return "ean";
                else if (checkingKey == '3')
                    return "name";
                else
                    Console.WriteLine(Resources.Language.WrongMenuInput);
            }
        }

        public static bool CheckIsEverythingCorrect()
        {
            char isEverythingCorrect;

            Console.WriteLine();
            Console.WriteLine(Resources.Language.IsEverythingCorrect);
            Console.WriteLine("1. " + Resources.Language.Yes);
            Console.WriteLine("2. " + Resources.Language.No);
            
            while (true)
            {
                isEverythingCorrect = Console.ReadKey(true).KeyChar;

                if (isEverythingCorrect == '1')
                    return true;
                else if (isEverythingCorrect == '2')
                    return false;
                else
                    Console.WriteLine(Resources.Language.WrongMenuInput);
            }
        }

        public static string CallBaseLinker_SuccessOrError(string tokenAPI, string method, string parameters)
        {
            var values = new Dictionary<string, string>
                {
                    { "token", tokenAPI },
                    { "method", method },
                    { "parameters", parameters }
                };

            var content = new FormUrlEncodedContent(values);
            var response = LogoAndMainMenu.client.PostAsync("https://api.baselinker.com/connector.php", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                Thread.Sleep(1000);
                dynamic responseObject = JObject.Parse(responseString);
                string responseStatus = responseObject.status;
                return responseStatus;
            }
            else
            {
                return "ERROR";
            }


        }

    }
}
