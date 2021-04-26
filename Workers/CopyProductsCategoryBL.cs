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
            //Individual worker inputs
            string tokenAPISource = GetTokenAPI();
            string categorySource = GetCategory();

            Console.WriteLine(Resources.Language.NowEnterSecondBL);

            string tokenAPITarget = GetTokenAPI();
            string category2Target = GetCategory();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPISource + ": " + tokenAPISource);
            Console.WriteLine(Resources.Language.YourOptionsCategorySource + ": " + categorySource);
            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptionsAPITarget + ": " + tokenAPITarget);
            Console.WriteLine(Resources.Language.YourOptionsCategoryTarget + ": " + category2Target);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;
            //

            //Get products list from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
            var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPISource, categorySource);
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

            Console.WriteLine("listOfProductsFull.Count() = " + listOfProductsFull.Count());

        }
    }
}
