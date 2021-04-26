using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using BaseLinker_Products_Multitool.Workers;
using System.Net.Http;


//TO DO: Service of errors (in example when sb try to type string when should type int)


namespace BaseLinker_Products_Multitool
{
    class LogoAndMainMenu
    {
        public static readonly HttpClient client = new HttpClient();
        static char MainMenu()
        {
            char menuInput;
            Console.WriteLine("================ "+ Resources.Language.MainMenu + " ================");
            Console.WriteLine("1. "+ Resources.Language.Menu1);
            Console.WriteLine("2. "+ Resources.Language.Menu2);
            Console.WriteLine();
            Console.WriteLine("3. " + Resources.Language.Menu3);
            Console.WriteLine("4. "+ Resources.Language.Menu4);
            Console.WriteLine();
            Console.WriteLine("5. " + Resources.Language.Menu5);
            Console.WriteLine();
            Console.WriteLine("6. "+ Resources.Language.Exit);

            menuInput = Console.ReadKey(true).KeyChar;
            return menuInput;
        }

        static void MainActivity()
        {
            Console.Clear();
            char menuInput;

            while (true)
            {
                menuInput = MainMenu();
                Console.Clear();
                switch (menuInput)
                {
                    case '1':
                        CheckDuplicates.CheckIsDuplicatesExist_Worker();
                        Console.Clear();
                        break;
                    case '2':
                        CheckAndDeleteDuplicates.CheckAndDeleteDuplicates_Worker();
                        Console.Clear();
                        break;
                    case '3':
                        CopyProductsCategoryBL.CopyCategoryProductsBetweenBaselinkerAccounts_Worker();
                        Console.Clear();
                        break;
                    case '4':
                        CopyWarehouseBL.CopyEntireWarehouseBetweenBaselinkerAccounts_Worker();
                        Console.Clear();
                        break;
                    case '5':
                        GenerateProducts.MassiveGenerateProducts_WorkerAsync();
                        Console.Clear();
                        break;
                    case '6':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(Resources.Language.WrongMenuInput + Environment.NewLine);
                        break;
                }
            }
        }

        static void Logo()
        {
            Console.Clear();
            Console.WriteLine("██████╗  █████╗ ███████╗███████╗██╗     ██╗███╗   ██╗██╗  ██╗███████╗██████╗ ");
            Console.WriteLine("██╔══██╗██╔══██╗██╔════╝██╔════╝██║     ██║████╗  ██║██║ ██╔╝██╔════╝██╔══██╗");
            Console.WriteLine("██████╔╝███████║███████╗█████╗  ██║     ██║██╔██╗ ██║█████╔╝ █████╗  ██████╔╝");
            Console.WriteLine("██╔══██╗██╔══██║╚════██║██╔══╝  ██║     ██║██║╚██╗██║██╔═██╗ ██╔══╝  ██╔══██╗");
            Console.WriteLine("██████╔╝██║  ██║███████║███████╗███████╗██║██║ ╚████║██║  ██╗███████╗██║  ██║");
            Console.WriteLine("╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝");
            Console.WriteLine("     ██████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗ ██████╗████████╗███████╗");
            Console.WriteLine("     ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██║   ██║██╔════╝╚══██╔══╝██╔════╝");
            Console.WriteLine("     ██████╔╝██████╔╝██║   ██║██║  ██║██║   ██║██║        ██║   ███████╗");
            Console.WriteLine("     ██╔═══╝ ██╔══██╗██║   ██║██║  ██║██║   ██║██║        ██║   ╚════██║");
            Console.WriteLine("     ██║     ██║  ██║╚██████╔╝██████╔╝╚██████╔╝╚██████╗   ██║   ███████║");
            Console.WriteLine("     ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝  ╚═════╝   ╚═╝   ╚══════╝");
            Console.WriteLine("          ███╗   ███╗██╗   ██╗██╗  ████████╗██╗████████╗ ██████╗  ██████╗ ██╗");
            Console.WriteLine("          ████╗ ████║██║   ██║██║  ╚══██╔══╝██║╚══██╔══╝██╔═══██╗██╔═══██╗██║     ");
            Console.WriteLine("          ██╔████╔██║██║   ██║██║     ██║   ██║   ██║   ██║   ██║██║   ██║██║     ");
            Console.WriteLine("          ██║╚██╔╝██║██║   ██║██║     ██║   ██║   ██║   ██║   ██║██║   ██║██║     ");
            Console.WriteLine("          ██║ ╚═╝ ██║╚██████╔╝███████╗██║   ██║   ██║   ╚██████╔╝╚██████╔╝███████╗");
            Console.WriteLine("          ╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝");
            Thread.Sleep(1500);
            Console.Clear();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 30);

            try
            {
                Logo();
                char input;

                Console.WriteLine("[EN] Select language | [PL] Wybierz język:");
                Console.WriteLine("1. PL");
                Console.WriteLine("2. EN");

                while (true)
                {
                    input = Console.ReadKey(true).KeyChar;

                    if (input == '1')
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
                        break;
                    }
                    else if (input == '2')
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("[EN] Press 1 or 2 for select language | [PL] Naciśnij 1 lub 2 aby wybrać język");
                    }
                }

                MainActivity();
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: "+ex.Message);
                Console.WriteLine("Click any button to close application...");
                Console.ReadKey();
            }
        }
    }
}
