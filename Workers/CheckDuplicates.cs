using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLinker_Products_Multitool.GlobalWorkers;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace BaseLinker_Products_Multitool
{
    class GetProductsListParameters
    {
        public string storage_id { get; set; }
        public string filter_category_id { get; set; }
        public int page { get; set; }
    }

    class ProductSimple
    {
        public string Sku { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }

        public ProductSimple(string sku, string ean, string name)
        {
            Sku = sku;
            Ean = ean;
            Name = name;
        }
    }

    class CheckDuplicates
    {
        public static void CheckIsDuplicatesExist_Worker()
        {
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();
            string checkingKey = GetCheckingKey();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPI + ": " + tokenAPI);
            Console.WriteLine(Resources.Language.YourOptionsCategory + ": " + category);
            Console.WriteLine(Resources.Language.YourOptionsCheckKey + ": " + checkingKey);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;

            List<ProductSimple> listOfProducts = new List<ProductSimple>();
            int page = 1;

            Console.WriteLine(Resources.Language.StartedGetProductsList);

            while (true)
            {
                int productsInPage = 0;
                GetProductsListParameters getProductsListParameters = new GetProductsListParameters()
                {
                    storage_id = "bl_1",
                    filter_category_id = category,
                    page = page
                };

                string parameters = JsonConvert.SerializeObject(getProductsListParameters);

                JObject response = CallBaseLinker(tokenAPI, "getProductsList", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    JArray products = (JArray)response["products"];
                    
                    foreach (var item in products)
                    {
                        string sku = item["sku"].ToString();
                        string ean = item["ean"].ToString();
                        string name = item["name"].ToString();

                        ProductSimple baseLinkerProduct = new ProductSimple(sku, ean, name);
                        listOfProducts.Add(baseLinkerProduct);
                        productsInPage++;
                    }

                    if (productsInPage == 0)
                        break;

                    Console.WriteLine(Resources.Language.DownloadedXProducts.Replace("{a}", listOfProducts.Count().ToString()));
                }
                else
                {
                    Console.WriteLine(Resources.Language.ErrorWhenDownloadProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                    Console.ReadKey();
                    return;
                }

                page++;
            }

            if(listOfProducts.Count() == 0)
            {
                Console.WriteLine(Resources.Language.EmptyListOfProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                Console.ReadKey();
                return;
            }

            List<string> duplicates = new List<string>();

            switch (checkingKey)
            {
                case "sku":
                    duplicates = listOfProducts.GroupBy(x => x.Sku)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
                    break;

                case "ean":
                    duplicates = listOfProducts.GroupBy(x => x.Ean)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
                    break;

                case "name":
                    duplicates = listOfProducts.GroupBy(x => x.Name)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
                    break;
            }



            if(duplicates.Count() > 0)
            {
                foreach (var item in duplicates)
                {
                    Console.WriteLine(Resources.Language.FoundDuplicate.Replace("{a}", checkingKey) + " '" + item + "'");
                }

                Console.WriteLine(Resources.Language.CheckDuplicatesEndInfo.Replace("{a}", listOfProducts.Count().ToString()).Replace("{b}", duplicates.Count().ToString()).Replace("{c}", checkingKey));
                Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine(Resources.Language.InfoNoDuplicates + " " + Resources.Language.PressAnythingToBackToMenu);
                Console.ReadKey();
                return;
            }

        }
    }
}
