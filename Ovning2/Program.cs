namespace Ovning2
{
    internal class Program
    {
        private static bool ExitProgram = false;
        static void Main(string[] args)
        {

            do
            {
                Console.Clear();
                MenuDescription();
                DisplayMenu();
                Menu();
            } while (!ExitProgram);
        }

        internal class MenuData
        {
            public const string CloseProgram = "0";
            public const string Prices = "1";
            public const string CalculateGroupPrice = "2";
            public const string RepeatMessage = "3";

            public static readonly string MenuDescription = "Välkommen till huvudmenyn!\nInstruktioner:\nTryck \"0\" för att avsluta programmet.\nTryck \"1\" för att få pris baserat på ålder.\nTryck \"2\" för att räkna ut pris för ett sällskap.\nTryck \"3\" för skriva in ett ord som sedan visas 10 gånger.\n";
            public static readonly string ReturnInstruction = "\nTryck enter för att återvända till menyn.";
        }

            internal class Prices
        {
            public static readonly uint Youth = 19;
            public static readonly uint Pensioner = 65;
            //public static readonly uint NoneStart = 20;
            //public static readonly uint NonEnd = 64;

            public static readonly uint YouthPrice = 80;
            public static readonly uint PensionerPrice = 90;
            public static readonly uint StandardPrice = 120;

            public static readonly string StringYouthPrice = "Ungdomspris: 80kr";
            public static readonly string StringPensionerPrice = "Pensionärspris: 90kr";
            public static readonly string StringStandarPrice = "Standarpris: 120kr";
        }

        private static void MenuDescription()
        {
            Console.WriteLine($"{MenuData.MenuDescription}");
        }

        private static void Menu()
        {
            string menuInput = Console.ReadLine()!;
            switch (menuInput)
            {
                case MenuData.CloseProgram:
                    ExitProgram = true;
                    break;
                case MenuData.Prices:
                    Console.Clear();
                    ShowPrices();
                    break;
                case MenuData.CalculateGroupPrice:
                    Console.Clear();
                    CalculateGroupPrice();
                    break;
                case MenuData.RepeatMessage:
                    Console.Clear();
                    RepeatMessage();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"Valet \"{menuInput}\" existerar inte.\n{MenuData.ReturnInstruction}");
                    Console.ReadLine();
                    break;
            }
            
        }

        private static void RepeatMessage()
        {
            string message = ValidateString();
            message = $"{message},";
            Console.Clear();
            for (int i = 0;i != 10; i++) 
            {
                if (i == 9)
                {
                    message = String.Concat(message.Replace(",", ""));
                }

                Console.Write($"{i+1}.{message}");
            }
            Console.WriteLine($"\n{MenuData.ReturnInstruction}");
            Console.ReadLine();
        }

        private static void CalculateGroupPrice()
        {
            Console.WriteLine("Antal personer:");
            uint GroupMembers = StringToUint();
            uint TotalPrice = 0;

            for (uint member = 0; member != GroupMembers; member++)
            {
                Console.WriteLine($"Ålder, Person {member+1}:");
                TotalPrice +=  GetPrices();
            }
            Console.Clear();
            Console.WriteLine($"Pris: {TotalPrice}kr \nAntal personer:{GroupMembers}\n{MenuData.ReturnInstruction}");
            Console.ReadLine();

        }

        private static void ShowPrices()
        {
            Console.WriteLine($"Skriv in ålder:");
            uint age = StringToUint();
            if (age <= Prices.Youth)
            {
                Console.WriteLine($"{Prices.StringYouthPrice}\n{MenuData.ReturnInstruction}");
            }
            else if (age >= Prices.Pensioner)
            {
                Console.WriteLine($"{Prices.StringPensionerPrice}\n{MenuData.ReturnInstruction}");
            }
            else
            {
                Console.WriteLine($"{Prices.StringStandarPrice}\n{MenuData.ReturnInstruction}");
            }
            Console.ReadLine();
        }

        private static uint GetPrices()
        {
            uint age = StringToUint();
            uint price;
            if (age <= Prices.Youth)
            {
                price = Prices.YouthPrice;
            }
            else if (age >= Prices.Pensioner)
            {
                price = Prices.PensionerPrice;
            }
            else
            {
                price = Prices.StandardPrice;
            }

            return price;
        }


        private static void DisplayMenu()
        {
            Console.WriteLine("Meny:");
            Console.WriteLine($"{MenuData.CloseProgram}. Stäng programmet.");
            Console.WriteLine($"{MenuData.Prices}. Prisinformation baserat på ålder.");
            Console.WriteLine($"{MenuData.CalculateGroupPrice}. Beräkna kostnad för ett sällskap.");
            Console.WriteLine($"{MenuData.RepeatMessage}. Upprepa meddelande.");
        }

        /// <summary>
        /// This method returns uint value from string input (with use of Console.ReadLine()). 
        /// </summary>
        /// <returns>
        /// <c>uintNumber</c> User written value converted to uint.
        /// </returns>
        private static uint StringToUint()
        {
            uint uintNumber;
            bool ParsingPassed = false;
            do
            {
                string StringInput = Console.ReadLine()!;
                (ParsingPassed, uintNumber) = ValidateStringToUint(StringInput);
            } while (ParsingPassed == false);

            return uintNumber;

        }
        /// <summary>
        /// This method validates whether string input consist of numbers only.
        /// </summary>
        /// <param>
        /// <c>numberInput</c> is the string input for validation.
        /// </param>
        /// <returns>
        /// <c>validationPassed</c> True if the input of <c>numberInput</c> consist of 
        /// numbers only; otherwise returns false. 
        /// <c>number</c> is the string input converted to int, if string contains
        /// illegal characters the value defaults to 0.
        /// </returns>
        public static (bool, uint) ValidateStringToUint(string prompt)
        {
            uint number = 0;
            bool validationPassed = false;
            //check if number contains valid characters (numbers).
            if (!uint.TryParse(prompt, out number))
            {
                Console.WriteLine($"\"{prompt}\" is not a valid input, use numbers only:");
            }
            else
            {
                validationPassed = true;
            }
            // If parsing failed, number will deafult to 0 and validationPassed set to false,
            // else number will be assigned the expected int value and validationPassed true.

            return (validationPassed, number);
        }

        // Would be interesting to make generic function passed as parameter is possible through delegate use
        // "Func<inputparameter, returntype> myMethodName"as input parameter, next step is to find out how to mape parameters and returntypes generic.
        public static string ValidateString()
        {
            bool sucess = false;
            string answer;

            do
            {
                answer = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(answer) || answer.Contains(" "))
                {
                    //Console.WriteLine("Invalid input: You must enter a word, no spacing are allowed.");
                    Console.WriteLine("Fela: Yo");
                }
                else
                {
                    sucess = true;
                }
            } while (!sucess);

            return answer;
        }
    }
}
   