using Newtonsoft.Json;
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



        public static (bool, List<ProductSimple>) GetProductsListSimple(string tokenAPI, string category)
        {
            int page = 1;
            List<ProductSimple> listOfProducts = new List<ProductSimple>();

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
                        string id = item["product_id"].ToString();
                        string sku = item["sku"].ToString();
                        string ean = item["ean"].ToString();
                        string name = item["name"].ToString();

                        ProductSimple baseLinkerProduct = new ProductSimple(id, sku, ean, name);
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
                    return (false, null);
                }

                page++;
            }

            if (listOfProducts.Count() == 0)
            {
                Console.WriteLine(Resources.Language.EmptyListOfProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                Console.ReadKey();
                return (false, null);
            }

            return (true, listOfProducts);
        }

        public static List<string> CheckProductsDuplicates(List<ProductSimple>listOfProducts, string checkingKey)
        {
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
            return duplicates;
        }

        public static (bool, int, int) DeleteDuplicates(List<ProductSimple> listOfProducts, string checkingKey, string tokenAPI)
        {
            List<string> listOfAlreadyCheckedProducts = new List<string>();
            List<ProductSimple> listProductsToDelete = new List<ProductSimple>();

            foreach (ProductSimple singleProduct in listOfProducts)
            {
                string tempKey;
                switch (checkingKey)
                {
                    case "sku":
                        tempKey = singleProduct.Sku;
                        break;
                    case "ean":
                        tempKey = singleProduct.Ean;
                        break;
                    case "name":
                        tempKey = singleProduct.Name;
                        break;
                    default:
                        Console.WriteLine(Resources.Language.DownloadedXProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                        Console.ReadKey();
                        return (false, 0, 0);
                }

                if (!listOfAlreadyCheckedProducts.Contains(tempKey))
                {
                    listOfAlreadyCheckedProducts.Add(tempKey);
                }
                else
                {
                    listProductsToDelete.Add(singleProduct);
                }
            }

            int countAllProductsToDelete = listProductsToDelete.Count();

            bool IsEverythingCorrectDeleteProducts = CheckIsEverythingCorrectDeleteProducts(countAllProductsToDelete);

            if (!IsEverythingCorrectDeleteProducts)
                return (false, 0, 0);

            Console.WriteLine();
            Console.WriteLine(Resources.Language.StartedDeleteProduct);

            int productNumber = 1;
            int quantityOfSuccessResponses = 0;

            foreach (ProductSimple productToDelete in listProductsToDelete)
            {
                Console.WriteLine(productNumber + "/" + countAllProductsToDelete + " ...");

                DeleteProductParameters deleteProductParameters = new DeleteProductParameters()
                {
                    storage_id = "bl_1",
                    product_id = productToDelete.Id
                };

                string parameters = JsonConvert.SerializeObject(deleteProductParameters);

                JObject response = CallBaseLinker(tokenAPI, "deleteProduct", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(productNumber + "/" + countAllProductsToDelete + " OK!");
                    quantityOfSuccessResponses++;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(productNumber + "/" + countAllProductsToDelete + " " + Resources.Language.Error);
                }

                productNumber++;
            }
            return (true, countAllProductsToDelete, quantityOfSuccessResponses);
        }

        public static int GenerateNewProducts(ulong quantityOfNewProducts, string tokenAPI, string category)
        {
            Console.WriteLine();
            int quantityOfSuccessResponses = 0;

            for (ulong i = 1; i <= quantityOfNewProducts; i++)
            {
                Console.WriteLine(i + "/" + quantityOfNewProducts + " ...");

                AddProductParameters addProductParameters = new AddProductParameters()
                {
                    storage_id = "bl_1",
                    product_id = "",
                    sku = "sku" + i,
                    name = "Product" + i,
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
                    Console.WriteLine(i + "/" + quantityOfNewProducts + " " + Resources.Language.Error);
                }
            }

            return quantityOfSuccessResponses;
        }




        }
}
