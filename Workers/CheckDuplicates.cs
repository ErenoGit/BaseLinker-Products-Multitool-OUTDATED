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
    class CheckDuplicates
    {
        public static void CheckIsDuplicatesExist_Worker()
        {
            //individual worker inputs
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
            var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPI, category);
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
            //

        }
    }
}