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
            //Individual worker inputs
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
            //

            //Get products list from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPI, StringToIntOrDefault(category, -1));
            if (returnFromGetProductsListSimple.Item1 == false)
                return;
            List<ProductSimple> listOfProducts = returnFromGetProductsListSimple.Item2;
            //

            //Get list of duplicates
            List<string> duplicates = CheckProductsDuplicates(listOfProducts, checkingKey);
            //

            //Info about duplicates
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
            //

            //Delete duplicates, returns:     Item1 - is everything ok     Item2 - count of all products to delete     Item3 - quantity of success responses (successful deleted)
            var resultDeleteDuplicates = DeleteDuplicates(listOfProducts, checkingKey, tokenAPI);
            if (resultDeleteDuplicates.Item1 == false)
                return;
            int countAllProductsToDelete = resultDeleteDuplicates.Item2;
            int quantityOfSuccessResponses = resultDeleteDuplicates.Item3;
            //

            //End info
            Console.WriteLine();
            Console.WriteLine(Resources.Language.DeletedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", countAllProductsToDelete.ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
            Console.ReadKey();
            //

        }

    }
}
