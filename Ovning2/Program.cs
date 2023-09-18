using System;
using System.Linq.Expressions;

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
            public const string FindTheThirdWord = "4";

            public static readonly string MenuDescription = "Välkommen till huvudmenyn!\nInstruktioner:\nTryck \"0\" för att avsluta programmet.\nTryck \"1\" för att få pris baserat på ålder.\nTryck \"2\" för att räkna ut pris för ett sällskap.\nTryck \"3\" för skriva in ett ord som sedan visas 10 gånger.\nTryck \"4\" För att låta programmet hitta det tredje ordet i en mening.\n";
            public static readonly string ReturnInstruction = "\nTryck enter för att återvända till menyn.";
            public static readonly string errorMessageString = "Felaktig inmatning. Meddelandet får ej lämnas tomt eller bestå av enbart mellanslag. Skriv in nytt meddelande:";
            public static readonly string errorMessageUint = " är felaktig inmatning. Endast användning av positiva heltal tillåts, skriv in antalet med använding av siffror:";
            public static readonly string errorMessageFindTheThirdWord = " är felaktig inmatning. Skriv in minst tre ord separera orden med enbart ett mellanslag:";
        }

            internal class Prices
        {
            public static readonly uint Youth = 19;
            public static readonly uint Pensioner = 65;

            public static readonly uint YouthPrice = 80;
            public static readonly uint PensionerPrice = 90;
            public static readonly uint StandardPrice = 120;

            public static readonly string StringYouthPrice = "Ungdomspris: 80kr";
            public static readonly string StringPensionerPrice = "Pensionärspris: 90kr";
            public static readonly string StringStandarPrice = "Standarpris: 120kr";
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Meny:");
            Console.WriteLine($"{MenuData.CloseProgram}. Stäng programmet.");
            Console.WriteLine($"{MenuData.Prices}. Prisinformation baserat på ålder.");
            Console.WriteLine($"{MenuData.CalculateGroupPrice}. Beräkna kostnad för ett sällskap.");
            Console.WriteLine($"{MenuData.RepeatMessage}. Upprepa meddelande.");
            Console.WriteLine($"{MenuData.FindTheThirdWord}. Programmet hittar det tredje ordet i en mening.");
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
                case MenuData.FindTheThirdWord:
                    Console.Clear();
                    FindTheThirdWord();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"Valet \"{menuInput}\" existerar inte.\n{MenuData.ReturnInstruction}");
                    Console.ReadLine();
                    break;
            }
            
        }

        private static void FindTheThirdWord()
        {
            Console.WriteLine("Skriv in en mening med minst tre ord separerade med ett mellanslag:");
            string theThirdWord = ValidateSentence();
            Console.Clear();
            Console.WriteLine($"Det tredje ordet är: {theThirdWord}");
            Console.WriteLine($"\n{MenuData.ReturnInstruction}");
            Console.ReadLine();

        }

        private static string ValidateSentence()
        {
            string[] words = new string[3];
            bool inputValid = false;
            do
            {
                //Check if string is null or consist of only whitespace or null.
                string sentenceInput = ValidateString(MenuData.errorMessageFindTheThirdWord);
                // Check if the sentence contains number of wanted word (no word is null or space).
                (inputValid, words) = StringToArray(sentenceInput, 3, inputValid);
                //Return the third word when checking is completed.
            } while (inputValid == false);

            return words[2];
        }

        //Validates a choosen number of words and returns them separated in an array.
        private static (bool, string[]) StringToArray(string sentenceInput, uint numberOfWords,bool inputValid)
        {
                string[] words;
                words = sentenceInput.Split(" ");
                // checks if there is a word missing, the word may still be blank space or null.
                if (words.Length < numberOfWords)
                {
                    Console.Clear();
                    Console.WriteLine($"Antalet ord({words.Length}) {MenuData.errorMessageFindTheThirdWord}");
                }
                else
                {
                    //Check if the characters in the words does not contain space or null.
                    inputValid = ValidateWords(words, MenuData.errorMessageFindTheThirdWord);
                }

            return (inputValid, words);
            
        }

        /// <summary>
        /// This method returns uint value from string input (with use of Console.ReadLine()). 
        /// </summary>
        /// <returns>
        /// <c>uintNumber</c> User written value converted to uint.
        /// </returns>

        private static void RepeatMessage()
        {
            Console.WriteLine("Skriv ditt meddelande:");
            string message = ValidateString(MenuData.errorMessageString);
            message = $"{message},";
            Console.Clear();
            for (int i = 0; i != 10; i++)
            {
                if (i == 9)
                {
                    message = String.Concat(message.Replace(",", ""));
                }

                Console.Write($"{i + 1}.{message}");
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
                Console.WriteLine($"Ålder, Person {member + 1}:");
                TotalPrice += GetPrices();
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
                Console.WriteLine($"\"{prompt}\"{MenuData.errorMessageUint}");
            }
            else
            {
                validationPassed = true;
            }
            // If parsing failed, number will deafult to 0 and validationPassed set to false,
            // else number will be assigned the expected int value and validationPassed true.

            return (validationPassed, number);
        }

        // Would be interesting to make generic, function/method can be passed as parameter through delegate use:
        // "Func<inputparameter, returntype> myMethodName"as input parameter, next step is to find out how to make
        // parameters and returntypes generic.
        public static string ValidateString(string prompt)
        {
            bool stringIsValid = false;
            string stringInput;

            do
            {
                stringInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(stringInput))
                {
                    Console.WriteLine($"\"{stringInput}\" {prompt}");
                }
                else
                {
                    stringIsValid = true;
                }
            } while (!stringIsValid);

            return stringInput;
        }

        public static bool ValidateWords(string[] StringArray, string prompt)
        {
            bool stringIsValid = true;

                foreach (var item in StringArray)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        Console.WriteLine($"\"{item}\" {prompt}");
                        stringIsValid = false;
                    break;
                    }
                }

            return stringIsValid;
        }
        
    
    }
}
   