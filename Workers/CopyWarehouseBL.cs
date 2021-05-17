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
    class CopyWarehouseBL
    {
        public static void CopyEntireWarehouseBetweenBaselinkerAccounts_Worker()
        {
            //Individual worker inputs
            string tokenAPISource = GetTokenAPI();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.NowEnterSecondBL);
            Console.WriteLine();

            string tokenAPITarget = GetTokenAPI();

            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptions + ":");
            Console.WriteLine(Resources.Language.YourOptionsAPISource + ": " + tokenAPISource);
            Console.WriteLine();
            Console.WriteLine(Resources.Language.YourOptionsAPITarget + ": " + tokenAPITarget);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;
            

            Console.WriteLine();

            bool IsEverythingCorrectInfoAboutImages = CheckIsEverythingCorrectInfoAboutImagesAndVariants();

            if (!IsEverythingCorrectInfoAboutImages)
                return;
            //

            var returnFromGetBLCategories = GetBLCategories(tokenAPISource);
            if (returnFromGetBLCategories.Item1 == false)
                return;
            List<Category> listOfCategories = returnFromGetBLCategories.Item2;

            (bool, Dictionary<int, int>) returnFromAddBLCategories = (false, new Dictionary<int, int>());

            if (listOfCategories == null || listOfCategories.Count() == 0)
            {
                Console.WriteLine(Resources.Language.EmptyCategoriesList + ": " + tokenAPITarget);
            }
            else
            {
                //Add categories to BL, returns:     Item1 - is everything ok     Item2 - dictionary<int,int> where item1 is source category id, item 2 is target category id
                returnFromAddBLCategories = AddBLCategories(listOfCategories, tokenAPITarget);
                if (returnFromAddBLCategories.Item1 == false)
                    return;
            }

            int quantityOfSuccessResponses = -1;
            List<ProductFull> listOfProductsFull = null;

            foreach (var categoryPair in returnFromAddBLCategories.Item2)
            {
                //Get products list from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
                var returnFromGetProductsListSimple = GetProductsListSimple(tokenAPISource, categoryPair.Key);
                if (returnFromGetProductsListSimple.Item1 == false)
                    return;
                List<ProductSimple> listOfProducts = returnFromGetProductsListSimple.Item2;
                //

                //Get products list (full products data) from selected BL category, returns:     Item1 - is everything ok     Item2 - list of products
                var returnGetProductsListFull = GetProductsListFull(listOfProducts, tokenAPISource, categoryPair.Key.ToString());
                if (returnGetProductsListFull.Item1 == false)
                    return;
                listOfProductsFull = returnGetProductsListFull.Item2;
                //

                //Add products, return quantity of success responses (successful created)
                quantityOfSuccessResponses = AddNewProducts(listOfProductsFull, tokenAPITarget, categoryPair.Value.ToString());
                //
            }

            //End info
            Console.WriteLine();
            Console.WriteLine(Resources.Language.AddedProductsInfo.Replace("{a}", quantityOfSuccessResponses.ToString()).Replace("{b}", listOfProductsFull.Count().ToString()));

            Console.WriteLine(Resources.Language.PressAnythingToBackToMenu);
            Console.ReadKey();
            //
        }
    }
}
