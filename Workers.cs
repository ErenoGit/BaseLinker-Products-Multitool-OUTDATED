using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinker_Products_Multitool
{
    class Workers
    {
        private static string GetTokenAPI()
        {
            Console.WriteLine(Resources.Language.EnterAPIToken);
            return Console.ReadLine();
        }

        private static string GetCategory()
        {
            Console.WriteLine("");
            Console.WriteLine(Resources.Language.EnterCategory);
            return Console.ReadLine();
        }

        private static string GetCheckingKey()
        {
            string checkingKey;
            Console.WriteLine("");
            Console.WriteLine(Resources.Language.HowToCheckDuplicates);
            Console.WriteLine("1. " + Resources.Language.HowToCheckDuplicates1 + " (sku)");
            Console.WriteLine("2. " + Resources.Language.HowToCheckDuplicates2 + " (ean)");
            Console.WriteLine("3. " + Resources.Language.HowToCheckDuplicates3 + " (name)");
            checkingKey = Console.ReadLine();
            switch (checkingKey)
            {
                case "1":
                    checkingKey = "sku";
                    break;
                case "2":
                    checkingKey = "ean";
                    break;
                case "3":
                    checkingKey = "name";
                    break;
                default:
                    checkingKey = "ERROR";
                    break;
            }

            return checkingKey;
        }

        private static bool CheckIsEverythingCorrect()
        {
            Console.WriteLine("");
            Console.WriteLine(Resources.Language.IsEverythingCorrect);
            Console.WriteLine("1. " + Resources.Language.Yes);
            Console.WriteLine("2. " + Resources.Language.No);
            string IsEverythingCorrect = Console.ReadLine();

            switch (IsEverythingCorrect)
            {
                case "1":
                    return true;
                case "2":
                    return false;
                default:
                    Console.WriteLine(Resources.Language.WrongMenuInput);
                    Console.ReadKey();
                    return false;
            }
        }



        public static void CheckIsDuplicatesExist()
        {
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();
            string checkingKey = GetCheckingKey();

            if (checkingKey == "ERROR")
            {
                Console.WriteLine(Resources.Language.WrongMenuInput);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("");
            Console.WriteLine(Resources.Language.YourOptions+":");
            Console.WriteLine(Resources.Language.YourOptionsAPI+": "+ tokenAPI);
            Console.WriteLine(Resources.Language.YourOptionsCategory+": "+ category);
            Console.WriteLine(Resources.Language.YourOptionsCheckKey+": "+ checkingKey);

            bool IsEverythingCorrect = CheckIsEverythingCorrect();

            if (!IsEverythingCorrect)
                return;

            

            //TO DO: CHECKING DUPLICATES HERE
        }

        public static void DeleteDuplicates()
        {

        }

        public static void CopyProductsBetweenBaselinkerAccounts()
        {

        }
    }
}
