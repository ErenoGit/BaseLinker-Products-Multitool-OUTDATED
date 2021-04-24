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
        public string id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }

        public ProductSimple(string _id, string _sku, string _ean, string _name)
        {
            id = _id;
            sku = _sku;
            ean = _ean;
            name = _name;
        }
    }

    class ProductFull
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price_netto { get; set; }
        public float price_brutto { get; set; }
        public float price_wholesale_netto { get; set; }
        public int tax_rate { get; set; }
        public float weight { get; set; }
        public string description { get; set; }
        public string description_extra1 { get; set; }
        public string description_extra2 { get; set; }
        public string description_extra3 { get; set; }
        public string description_extra4 { get; set; }
        public string man_name { get; set; }
        public string man_image { get; set; }
        public int category_id { get; set; }
        public Array images { get; set; }
        public Array features { get; set; }
        public Array variants { get; set; }

        public ProductFull(string _id, string _sku, string _ean, string _name, int _quantity, float _price_netto, float _price_brutto, float _price_wholesale_netto, int _tax_rate, float _weight, string _description, string _description_extra1, string _description_extra2, string _description_extra3, string _description_extra4, string _man_name, string _man_image, int _category_id, Array _images, Array _features, Array _variants)
        {
            id = _id;
            sku = _sku;
            ean = _ean;
            name = _name;
            quantity = _quantity;
            price_netto = _price_netto;
            price_brutto = _price_brutto;
            price_wholesale_netto = _price_wholesale_netto;
            tax_rate = _tax_rate;
            weight = _weight;
            description = _description;
            description_extra1 = _description_extra1;
            description_extra2 = _description_extra2;
            description_extra3 = _description_extra3;
            description_extra4 = _description_extra4;
            man_name = _man_name;
            man_image = _man_image;
            category_id = _category_id;
            images = _images;
            features = _features;
            variants = _variants;
        }
    }


    class GetProductsListParameters
    {
        public string storage_id { get; set; }
        public string filter_category_id { get; set; }
        public int page { get; set; }
        public GetProductsListParameters(string _storage_id, string _filter_category_id, int _page)
        {
            storage_id = _storage_id;
            filter_category_id = _filter_category_id;
            page = _page;
        }
    }

    class DeleteProductParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
        public DeleteProductParameters(string _storage_id, string _product_id)
        {
            storage_id = _storage_id;
            product_id = _product_id;
        }
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
        public AddProductParameters(string _storage_id, string _product_id, string _sku, string _name, int _quantity, float _price_brutto, int _tax_rate, int _category_id, string _man_name)
        {
            storage_id = _storage_id;
            product_id = _product_id;
            sku = _sku;
            name = _name;
            quantity = _quantity;
            price_brutto = _price_brutto;
            tax_rate = _tax_rate;
            category_id = _category_id;
            man_name = _man_name;
        }
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
                GetProductsListParameters getProductsListParameters = new GetProductsListParameters("bl_1", category, page);

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
                    duplicates = listOfProducts.GroupBy(x => x.sku)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
                    break;

                case "ean":
                    duplicates = listOfProducts.GroupBy(x => x.ean)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
                    break;

                case "name":
                    duplicates = listOfProducts.GroupBy(x => x.name)
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
                        tempKey = singleProduct.sku;
                        break;
                    case "ean":
                        tempKey = singleProduct.ean;
                        break;
                    case "name":
                        tempKey = singleProduct.name;
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

                DeleteProductParameters deleteProductParameters = new DeleteProductParameters("bl_1", productToDelete.id);

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

                AddProductParameters addProductParameters = new AddProductParameters("bl_1", "", "sku" + i, "Product" + i, 0, 0.0f, 0, Convert.ToInt32(category), "");

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

        public static (bool, List<ProductFull>) GetProductsListFull(List<ProductSimple> listOfProducts, string tokenAPI, string category)
        {
            int page = 1;
            List<ProductFull> listOfProductsFull = new List<ProductFull>();

            Console.WriteLine(Resources.Language.StartedGetProductsListFull);

            while (true)
            {
                int productsInPage = 0;
                GetProductsListParameters getProductsListParameters = new GetProductsListParameters("bl_1", category, page);

                string parameters = JsonConvert.SerializeObject(getProductsListParameters);

                JObject response = CallBaseLinker(tokenAPI, "getProductsData", parameters);
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
                        int quantity = int.Parse(item["quantity"].ToString());
                        float price_netto = float.Parse(item["price_netto"].ToString());
                        float price_brutto = float.Parse(item["price_brutto"].ToString());
                        float price_wholesale_netto = float.Parse(item["price_wholesale_netto"].ToString());
                        int tax_rate = int.Parse(item["tax_rate"].ToString());
                        float weight = float.Parse(item["weight"].ToString());
                        string description = item["description"].ToString();
                        string description_extra1 = item["description_extra1"].ToString();
                        string description_extra2 = item["description_extra2"].ToString();
                        string description_extra3 = item["description_extra3"].ToString();
                        string description_extra4 = item["description_extra4"].ToString();
                        string man_name = item["man_name"].ToString();
                        string man_image = item["man_image"].ToString();
                        int category_id = int.Parse(item["category_id"].ToString());
                        Array images = null;// item["images"];
                        Array features = null;// item["features"];
                        Array variants = null;// item["variants"];

                        ProductFull baseLinkerProduct = new ProductFull(id, sku, ean, name, quantity, price_netto, price_brutto, price_wholesale_netto, tax_rate, weight, description, description_extra1, description_extra2, description_extra3, description_extra4, man_name, man_image, category_id, images, features, variants);
                        listOfProductsFull.Add(baseLinkerProduct);
                        productsInPage++;
                    }

                    if (productsInPage == 0)
                        break;

                    Console.WriteLine(Resources.Language.DownloadedInfoAboutXProducts.Replace("{a}", listOfProductsFull.Count().ToString()));
                }
                else
                {
                    Console.WriteLine(Resources.Language.ErrorWhenDownloadFullProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                    Console.ReadKey();
                    return (false, null);
                }

                page++;
            }

            return (true, listOfProductsFull);
        }


    }
}
