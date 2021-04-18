using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BaseLinker_Products_Multitool.GlobalWorkers;
using System.Net.Http;
using System.Threading;

namespace BaseLinker_Products_Multitool.Workers
{
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

    class GenerateProducts
    {
        private static ulong GetQuantityOfNewProducts()
        {
            Console.WriteLine();
            Console.WriteLine(Resources.Language.EnterQuantityOfNewProducts + " (max 18446744073709551615)");
            return Convert.ToUInt64(Console.ReadLine());
        }

        public static void MassiveGenerateProducts_WorkerAsync()
        {
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();
            UInt64 quantityOfNewProducts = GetQuantityOfNewProducts();


            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPI + ": " + tokenAPI);
            Console.WriteLine(Resources.Language.YourOptionsCategory + ": " + category);
            Console.WriteLine(Resources.Language.YourOptionsCategory + ": " + quantityOfNewProducts);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;

            Console.WriteLine();
            int quantityOfSuccessResponses = 0;

            for (ulong i = 1; i <= quantityOfNewProducts; i++)
            {
                Console.WriteLine(i + "/" + quantityOfNewProducts+" ...");

                AddProductParameters addProductParameters = new AddProductParameters()
                {
                    storage_id = "bl_1",
                    product_id = "",
                    sku = "sku"+i,
                    name = "Product"+i,
                    quantity = 0,
                    price_brutto = 0.0f,
                    tax_rate = 0,
                    category_id = Convert.ToInt32(category),
                    man_name = ""
                };

                string parameters = JsonConvert.SerializeObject(addProductParameters);

                JObject response = CallBaseLinker(tokenAPI, "addProduct", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(i + "/" + quantityOfNewProducts + " OK!");
                    quantityOfSuccessResponses++;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(i + "/" + quantityOfNewProducts + " ERROR!");
                }


            }

            Console.WriteLine();
            Console.WriteLine(Resources.Language.AddedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", quantityOfNewProducts.ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);

            Console.ReadKey();
        }
    }
}
