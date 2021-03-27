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
            char checkingKey;

            Console.WriteLine("");
            Console.WriteLine(Resources.Language.HowToCheckDuplicates);
            Console.WriteLine("1. " + Resources.Language.HowToCheckDuplicates1 + " (sku)");
            Console.WriteLine("2. " + Resources.Language.HowToCheckDuplicates2 + " (ean)");
            Console.WriteLine("3. " + Resources.Language.HowToCheckDuplicates3 + " (name)");
            
            while (true)
            {
                checkingKey = Console.ReadKey(true).KeyChar;

                if(checkingKey == '1')
                    return "sku";
                else if (checkingKey == '2')
                    return "ean";
                else if (checkingKey == '3')
                    return "name";
                else
                    Console.WriteLine(Resources.Language.WrongMenuInput);
            }
        }

        private static bool CheckIsEverythingCorrect()
        {
            char isEverythingCorrect;

            Console.WriteLine("");
            Console.WriteLine(Resources.Language.IsEverythingCorrect);
            Console.WriteLine("1. " + Resources.Language.Yes);
            Console.WriteLine("2. " + Resources.Language.No);
            
            while (true)
            {
                isEverythingCorrect = Console.ReadKey(true).KeyChar;

                if (isEverythingCorrect == '1')
                    return true;
                else if (isEverythingCorrect == '2')
                    return false;
                else
                    Console.WriteLine(Resources.Language.WrongMenuInput);
            }
        }



        public static void CheckIsDuplicatesExist()
        {
            string tokenAPI = GetTokenAPI();
            string category = GetCategory();
            string checkingKey = GetCheckingKey();

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
