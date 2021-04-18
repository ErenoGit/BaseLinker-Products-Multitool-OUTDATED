using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLinker_Products_Multitool.GlobalWorkers;

namespace BaseLinker_Products_Multitool
{
    class CheckDuplicates
    {
        public static void CheckIsDuplicatesExist_Worker()
        {
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



            //TO DO: Sprawdzanie duplikatów produktów w konkretnej kategorii BL po mapowaniu wybranym wyżej
        }


    }
}
