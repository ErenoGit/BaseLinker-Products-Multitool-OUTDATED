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
    class GenerateProducts
    {

        public static void MassiveGenerateProducts_WorkerAsync()
        {
            //Individual worker inputs
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
            //

            //Delete duplicates, return quantity of success responses (successful created)
            int quantityOfSuccessResponses = GenerateNewProducts(quantityOfNewProducts, tokenAPI, category);
            //

            //End info
            Console.WriteLine();
            Console.WriteLine(Resources.Language.AddedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", quantityOfNewProducts.ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
            Console.ReadKey();
            //
        }
    }
}
