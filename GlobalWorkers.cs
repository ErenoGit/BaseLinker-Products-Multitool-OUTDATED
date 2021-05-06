using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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

    class Variant
    {
        public string variant_id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public string ean { get; set; }
        public string sku { get; set; }
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
        public List<String> images { get; set; }
        public List<Dictionary<string, string>> features { get; set; }
        public List<Variant> variants { get; set; }

        public ProductFull(string _id, string _sku, string _ean, string _name, int _quantity, float _price_netto, float _price_brutto, float _price_wholesale_netto, int _tax_rate, float _weight, string _description, string _description_extra1, string _description_extra2, string _description_extra3, string _description_extra4, string _man_name, string _man_image, int _category_id, List<String> _images, List<Dictionary<string, string>> _features, List<Variant> _variants)
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

    class GetProductsDataParameters
    {
        public string storage_id { get; set; }
        public Array products { get; set; }
        public GetProductsDataParameters(string _storage_id, Array _products)
        {
            storage_id = _storage_id;
            products = _products;
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

    class AddProductSimpleParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price_brutto { get; set; }
        public int tax_rate { get; set; }
        public int category_id { get; set; }
        public string man_name { get; set; }
        public AddProductSimpleParameters(string _storage_id, string _product_id, string _sku, string _ean, string _name, int _quantity, float _price_brutto, int _tax_rate, string _man_name, int _category_id)
        {
            storage_id = _storage_id;
            product_id = _product_id;
            sku = _sku;
            ean = _ean;
            name = _name;
            quantity = _quantity;
            price_brutto = _price_brutto;
            tax_rate = _tax_rate;
            man_name = _man_name;
            category_id = _category_id;
        }
    }

    class AddProductFullParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
        public string ean { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
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
        public int category_id { get; set; }
        public List<String> images { get; set; }
        public List<Dictionary<string, string>> features { get; set; }

        public AddProductFullParameters(string _storage_id, string _product_id, string _ean, string _sku, string _name, int _quantity, float _price_brutto, float _price_wholesale_netto, int _tax_rate, float _weight, string _description, string _description_extra1, string _description_extra2, string _description_extra3, string _description_extra4, string _man_name, int _category_id, List<String> _images, List<Dictionary<string, string>> _features)
        {
            storage_id = _storage_id;
            product_id = _product_id;
            ean = _ean;
            sku = _sku;
            name = _name;
            quantity = _quantity;
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
            category_id = _category_id;
            images = _images;
            features = _features;
        }
    }

    class AddProductVariantParameters
    {
        public string storage_id { get; set; }
        public string product_id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price_brutto { get; set; }
        public AddProductVariantParameters(string _storage_id, string _product_id, string _sku, string _ean, string _name, int _quantity, float _price_brutto)
        {
            storage_id = _storage_id;
            product_id = _product_id;
            sku = _sku;
            ean = _ean;
            name = _name;
            quantity = _quantity;
            price_brutto = _price_brutto;
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

        public static bool CheckIsEverythingCorrectInfoAboutImages()
        {
            char isEverythingCorrect;

            Console.WriteLine();
            Console.WriteLine(Resources.Language.InfoAboutImages);
            Console.WriteLine();
            Console.WriteLine(Resources.Language.InfoAboutVariantsPrices);
            Console.WriteLine();
            Console.WriteLine(Resources.Language.DoYouUnderstandAndAgree);
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

        public static int GenerateNewProducts(int quantityOfNewProducts, string tokenAPI, string category)
        {
            Console.WriteLine();
            int quantityOfSuccessResponses = 0;

            for (int i = 1; i <= quantityOfNewProducts; i++)
            {
                Console.WriteLine(i + "/" + quantityOfNewProducts + " ...");

                AddProductSimpleParameters addProductParameters = new AddProductSimpleParameters("bl_1", "", "sku" + i, i.ToString(), "Product" + i, 0, 0.0f, 0, "", Convert.ToInt32(category));

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

        public static int AddNewProducts(List<ProductFull> newProducts, string tokenAPI, string category)
        {
            Console.WriteLine();
            int quantityOfSuccessResponses = 0;
            int i = 1;
            foreach (ProductFull product in newProducts)
            {
                Console.WriteLine(i + "/" + newProducts.Count() + " ...");

                AddProductFullParameters addProductParameters = new AddProductFullParameters("bl_1", "", product.ean, product.sku, product.name, product.quantity, product.price_brutto, product.price_wholesale_netto, product.tax_rate, product.weight, product.description, product.description_extra1, product.description_extra2, product.description_extra3, product.description_extra4, product.man_name, Convert.ToInt32(category), product.images, product.features);

                string parameters = JsonConvert.SerializeObject(addProductParameters);

                JObject response = CallBaseLinker(tokenAPI, "addProduct", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(i + "/" + newProducts.Count() + " OK!");
                    quantityOfSuccessResponses++;

                    JValue responseProductId = (JValue)response["product_id"];
                    AddProductVariants(product, tokenAPI, responseProductId.Value.ToString());
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(i + "/" + newProducts.Count() + " " + Resources.Language.Error);
                }

                i++;
            }

            return quantityOfSuccessResponses;
        }

        public static void AddProductVariants(ProductFull product, string tokenAPI, string newProductId)
        {
            int i = 1;
            foreach (Variant variant in product.variants)
            {
                Console.WriteLine(Resources.Language.Variant + " " + i + "/" + product.variants.Count() + " ...");

                AddProductVariantParameters addProductParameters = new AddProductVariantParameters("bl_1", newProductId, variant.sku, variant.ean, variant.name, variant.quantity, variant.price);

                string parameters = JsonConvert.SerializeObject(addProductParameters);

                JObject response = CallBaseLinker(tokenAPI, "addProductVariant", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(Resources.Language.Variant + " " + i + "/" + product.variants.Count() + " OK!");
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(Resources.Language.Variant + " " + i + "/" + product.variants.Count() + " " + Resources.Language.Error);
                }

                i++;
            }
        }

        public static string GetStringOrNull(JToken item, string value)
        {
            object obj = item[value].ToObject<string>();

            if (obj == null)
                return null;
            else
                return item[value].ToString();
        }

        public static (bool, List<ProductFull>) GetProductsListFull(List<ProductSimple> listOfProducts, string tokenAPI, string category)
        {
            List<string> listOfProductsIds = new List<string>();
            foreach (var item in listOfProducts)
            {
                listOfProductsIds.Add(item.id);
            }
            List<List<string>> splittedListOfProductsIds = SplitList(listOfProductsIds, 1000);


            List<ProductFull> listOfProductsFull = new List<ProductFull>();

            Console.WriteLine(Resources.Language.StartedGetProductsListFull);

            foreach (List<string> listOf1000ProductsIds in splittedListOfProductsIds)
            {
                Array productsArray = listOf1000ProductsIds.ToArray();
                GetProductsDataParameters getProductsDataParameters = new GetProductsDataParameters("bl_1", productsArray);

                string parameters = JsonConvert.SerializeObject(getProductsDataParameters);

                JObject response = CallBaseLinker(tokenAPI, "getProductsData", parameters);
                JValue responseStatus = (JValue)response["status"];

                if (responseStatus.Value.ToString() == "SUCCESS")
                {
                    JArray product = new JArray();
                    foreach (JToken item in response["products"].Children().Children())
                    {
                        string id = item["product_id"].ToString();
                        string sku = GetStringOrNull(item, "sku");
                        string ean = GetStringOrNull(item, "ean");
                        string name = item["name"].ToString();
                        int quantity = int.Parse(item["quantity"].ToString());
                        float price_netto = float.Parse(item["price_netto"].ToString());
                        float price_brutto = float.Parse(item["price_brutto"].ToString());
                        float price_wholesale_netto = float.Parse(item["price_wholesale_netto"].ToString());
                        int tax_rate = int.Parse(item["tax_rate"].ToString());
                        float weight = float.Parse(item["weight"].ToString());
                        string description = GetStringOrNull(item, "description");
                        string description_extra1 = GetStringOrNull(item, "description_extra1");
                        string description_extra2 = GetStringOrNull(item, "description_extra2");
                        string description_extra3 = GetStringOrNull(item, "description_extra3");
                        string description_extra4 = GetStringOrNull(item, "description_extra4");
                        string man_name = GetStringOrNull(item, "man_name");
                        string man_image = GetStringOrNull(item, "man_image");
                        int category_id = int.Parse(item["category_id"].ToString());

                        List<String> images = null;
                        if (item["images"].HasValues) images = item["images"].Values<string>().ToList();
                        images = images.Select(filename => Path.Combine("url:", filename)).ToList();

                        List<Dictionary<string, string>> features = new List<Dictionary<string, string>>();
                        if (item["features"].HasValues)
                        {
                            foreach (JToken feature in item["features"].ToList())
                            {
                                features.Add(new Dictionary<string, string> { ["name"] = feature.First.ToString(), ["value"] = feature.Last.ToString() }); ;
                            }
                        }

                        List<Variant> variants = new List<Variant>();
                        if (item["variants"].HasValues)
                        {
                            foreach (JToken singleVariant in item["variants"].ToList())
                            {
                                Variant variant = new Variant();

                                foreach (JToken elementOfFeature in singleVariant)
                                {
                                    JProperty jProperty = elementOfFeature.ToObject<JProperty>();
                                    string propertyName = jProperty.Name;
                                    switch (propertyName)
                                    {
                                        case "variant_id":
                                            variant.variant_id = jProperty.Value.ToString();
                                            break;
                                        case "ean":
                                            variant.ean = jProperty.Value.ToString();
                                            break;
                                        case "sku":
                                            variant.sku = jProperty.Value.ToString();
                                            break;
                                        case "name":
                                            Regex regex = new Regex(name + " ");
                                            variant.name = regex.Replace(jProperty.Value.ToString().Replace(name,""), "", 1);
                                            break;
                                        case "price":
                                            variant.price = float.Parse(jProperty.Value.ToString());
                                            break;
                                        case "quantity":
                                            variant.quantity = int.Parse(jProperty.Value.ToString());
                                            break;
                                    }
                                }

                                variants.Add(variant);
                            }
                        }


                        ProductFull baseLinkerProduct = new ProductFull(id, sku, ean, name, quantity, price_netto, price_brutto, price_wholesale_netto, tax_rate, weight, description, description_extra1, description_extra2, description_extra3, description_extra4, man_name, man_image, category_id, images, features, variants);
                        listOfProductsFull.Add(baseLinkerProduct);
                    }

                    Console.WriteLine(Resources.Language.DownloadedInfoAboutXProducts.Replace("{a}", listOfProductsFull.Count().ToString()));
                }
                else
                {
                    Console.WriteLine(Resources.Language.ErrorWhenDownloadFullProducts + " " + Resources.Language.PressAnythingToBackToMenu);
                    Console.ReadKey();
                    return (false, null);
                }
            }

            return (true, listOfProductsFull);
        }

        public static List<List<string>> SplitList(List<string> locations, int nSize = 1000)
        {
            var list = new List<List<string>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }

        public static int GetQuantityOfNewProducts()
        {
            Console.WriteLine();
            Console.WriteLine(Resources.Language.EnterQuantityOfNewProducts + " (max 2147483647)");
            return Convert.ToInt32(Console.ReadLine());
        }

    }
}
