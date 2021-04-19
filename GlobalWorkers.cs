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
    class ProductSimple
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }

        public ProductSimple(string id, string sku, string ean, string name)
        {
            Id = id;
            Sku = sku;
            Ean = ean;
            Name = name;
        }
    }

    class GetProductsListParameters
    {
        public string storage_id { get; set; }
        public string filter_category_id { get; set; }
        public int page { get; set; }
    }

    class DeleteProductParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
    }

    class AddProductParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price_brutto { get; set; }
        public int tax_rate { get; set; }
        public int category_id { get; set; }
        public string man_name { get; set; }
    }

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

        public static bool GetDeleteKey()
        {
            char checkingKey;

            Console.WriteLine();
            Console.WriteLine(Resources.Language.HowToDeleteDuplicates);
            Console.WriteLine("1. " + Resources.Language.HowToDeleteDuplicates1);
            Console.WriteLine("2. " + Resources.Language.HowToDeleteDuplicates2);

            while (true)
            {
                checkingKey = Console.ReadKey(true).KeyChar;

                if (checkingKey == '1')
                    return true;
                else if (checkingKey == '2')
                    return false;
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

        public static bool CheckIsEverythingCorrectDeleteProducts(int quantity)
        {
            char isEverythingCorrect;

            Console.WriteLine();
            Console.WriteLine(Resources.Language.NumberOfProductsToDeleteQuestion+": "+ quantity); 
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

        

        public static JObject CallBaseLinker(string tokenAPI, string method, string parameters)
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
                JObject responseObject = JObject.Parse(responseString);
                return responseObject;
            }
            else
            {
                return new JObject(new JProperty("status", "ERROR"));
            }


        }

    }
}
