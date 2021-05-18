using System;
using static BaseLinker_Products_Multitool.GlobalWorkers;

namespace BaseLinker_Products_Multitool.Workers
{
    class GenerateProducts
    {

        public static void MassiveGenerateProducts_WorkerAsync()
        {
            //Individual worker inputs
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();
            int quantityOfNewProducts = GetQuantityOfNewProducts();


            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPI + ": " + tokenAPI);
            Console.WriteLine(Resources.Language.YourOptionsCategory + ": " + category);
            Console.WriteLine(Resources.Language.QuantityOfNewProducts + ": " + quantityOfNewProducts);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;
            //

            //Delete duplicates, return quantity of success responses (successful deleted)
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
