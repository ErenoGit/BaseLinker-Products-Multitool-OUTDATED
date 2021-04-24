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
    class CopyProductsCategoryBL
    {
        public static void CopyCategoryProductsBetweenBaselinkerAccounts_Worker()
        {
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();

            Console.WriteLine(Resources.Language.NowEnterSecondBL);

            string tokenAPI2 = GetTokenAPI();
            string category2 = GetCategory();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPI + ": " + tokenAPI);
            Console.WriteLine(Resources.Language.YourOptionsCategory + ": " + category);
            Console.WriteLine(Resources.Language.YourOptionsAPI2 + ": " + tokenAPI2);
            Console.WriteLine(Resources.Language.YourOptionsCategory2 + ": " + category2);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;

            //Get products list from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPI, category);
            if (returnFromGetProductsListSimple.Item1 == false)
                return;
            List<ProductSimple> listOfProducts = returnFromGetProductsListSimple.Item2;
            //

            //Get products list (full products data) from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnGetProductsListFull = GetProductsListFull(listOfProducts, tokenAPI, category);
            if (returnGetProductsListFull.Item1 == false)
                return;
            List<ProductFull> listOfProductsFull = returnGetProductsListFull.Item2;
            //


        }
    }
}
