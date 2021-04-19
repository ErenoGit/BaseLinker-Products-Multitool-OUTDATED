using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLinker_Products_Multitool.GlobalWorkers;

namespace BaseLinker_Products_Multitool
{
    class CheckAndDeleteDuplicates
    {
        public static void CheckAndDeleteDuplicates_Worker()
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
                    return;
                }

                page++;
            }

            if (listOfProducts.Count() == 0)
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



            if (duplicates.Count() > 0)
            {
                foreach (var item in duplicates)
                {
                    Console.WriteLine(Resources.Language.FoundDuplicate.Replace("{a}", checkingKey) + " '" + item + "'");
                }

                Console.WriteLine(Resources.Language.CheckDuplicatesEndInfo.Replace("{a}", listOfProducts.Count().ToString()).Replace("{b}", duplicates.Count().ToString()).Replace("{c}", checkingKey));
            }
            else
            {
                Console.WriteLine(Resources.Language.InfoNoDuplicates + " " + Resources.Language.PressAnythingToBackToMenu);
                Console.ReadKey();
                return;
            }

            bool reverseCheckingDuplicatesForDelete = GetDeleteKey();

            if (reverseCheckingDuplicatesForDelete)
                listOfProducts.Reverse();

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
                        return;
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
                return;

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

            Console.WriteLine();
            Console.WriteLine(Resources.Language.DeletedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", countAllProductsToDelete.ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
            Console.ReadKey();

        }

    }
}
