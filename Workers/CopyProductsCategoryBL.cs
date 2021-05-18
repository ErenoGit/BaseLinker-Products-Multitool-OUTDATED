using System;
using System.Collections.Generic;
using System.Linq;
using static BaseLinker_Products_Multitool.GlobalWorkers;

namespace BaseLinker_Products_Multitool
{
    class CopyProductsCategoryBL
    {
        public static void CopyCategoryProductsBetweenBaselinkerAccounts_Worker()
        {
            //Individual worker inputs
            Console.WriteLine(Resources.Language.EnterSourceCategory);
            Console.WriteLine();

            string tokenAPISource = GetTokenAPI();
            string categorySource = GetCategory();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.NowEnterTargetCategory);
            Console.WriteLine();

            string tokenAPITarget = GetTokenAPI();
            string categoryTarget = GetCategory();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPISource + ": " + tokenAPISource);
            Console.WriteLine(Resources.Language.YourOptionsCategorySource + ": " + categorySource);
            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptionsAPITarget + ": " + tokenAPITarget);
            Console.WriteLine(Resources.Language.YourOptionsCategoryTarget + ": " + categoryTarget);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;     

            Console.WriteLine();

            bool IsEverythingCorrectInfoAboutImages = CheckIsEverythingCorrectInfoAboutImagesAndVariants();

            if (!IsEverythingCorrectInfoAboutImages)
                return;
            //

            //Get products list from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPISource, StringToIntOrDefault(categorySource, -1));
            if (returnFromGetProductsListSimple.Item1 == false)
                return;
            List<ProductSimple> listOfProducts = returnFromGetProductsListSimple.Item2;
            //

            //Get products list (full products data) from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnGetProductsListFull = GetProductsListFull(listOfProducts, tokenAPISource, categorySource);
            if (returnGetProductsListFull.Item1 == false)
                return;
            List<ProductFull> listOfProductsFull = returnGetProductsListFull.Item2;
            //

            //Add products, return quantity of success responses (successful created)
            int quantityOfSuccessResponses = AddNewProducts(listOfProductsFull, tokenAPITarget, categoryTarget);
            //

            //End info
            Console.WriteLine();
            Console.WriteLine(Resources.Language.AddedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", listOfProductsFull.Count().ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
            Console.ReadKey();
            //

        }
    }
}
