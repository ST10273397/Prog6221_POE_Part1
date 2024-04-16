using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Speech.Synthesis;
using System.Net.Http;
using System.Media;

namespace Prog6221_POE
{
    internal class Recipes
    {
        public string RecipeName { get; set; }

        public int NumOfIngredients { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public List<Step> Steps { get; set; } = new List<Step>();

        public int NumOfSteps { get; set; }

        public double Scale { get; set; }

        SpeechSynthesizer talk = new SpeechSynthesizer();

        public bool Talk {  get; set; }

        public string message { get; set; }

        public System.Media.SoundPlayer MusicMan = new SoundPlayer();

        public Recipes() { }

        public Recipes(string recipeName, int numOfIngredients, List<Ingredient> ingredients, List<Step> steps, int stepNum, double scale)
        {
            RecipeName = recipeName;
            Ingredients = ingredients;
            NumOfIngredients = numOfIngredients;
            Steps = steps;
            NumOfSteps = stepNum;
            Scale = scale;
        }

        public void Settings()
        {
            Console.WriteLine("Would you like text-to-speech? (yes/no)");
            talk.Speak("Would you like text-to-speech? (yes/no)");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "yes") 
            {
                Talk = true;
                message = "Text-to-speech is now enabled.";
                Console.WriteLine(message);
                talk.Speak(message);
            }
            else
            {
                Talk = false;
            }
            message = "Would you like dark or light mode? Please type in only 'dark' or 'light'.";
            Console.WriteLine(message);
            Speak(message);
            answer = Console.ReadLine();
            Console.WriteLine("-------------------------------------------------------------------------------");
            if (answer == "light")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

            }
            message = "Would you like music? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            answer = Console.ReadLine();
            if (answer.ToLower() == "yes")
            {
                this.MusicMan = new SoundPlayer(@"C:\Users\nicho\source\repos\Prog6221_POE\Prog6221_POE\Camille, Michael Giacchino - Le Festin (From ＂Ratatouille＂).wav");
                this.MusicMan.PlayLooping();
            }
        }

        public void Speak(string message)
        {
            if (Talk)
            {
                talk.Speak(message);
            }
        }

        public void InputIngredients(int numOfIngredients)
        {
            string name = null;
            Console.WriteLine("------------------------------------------------------------------------------");
            message = "What is the first ingredient's name?";
            Console.WriteLine(message);
            Speak(message);
            name = StringCheck();
            message = "What unit of measurement(tsp/tbsp/cups) will the ingredient use?";
            Console.WriteLine(message);
            Speak(message);
            string unit = Console.ReadLine();
            message = "How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Measurement)";
            Console.WriteLine(message);
            Speak(message);
            double quantity = 0;
            while (quantity <= 0)
            {
                try
                {
                    quantity = Double.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Speak($"{ex.Message}");
                    message = "Please Type in a Number. No Letters, Words or Phrases.";
                    Console.WriteLine(message);
                    Speak(message);
                    continue;
                }
            }
            Ingredient newIngredient = new Ingredient { name = name, quantity = quantity, unit = unit, scale = 1 };
            Ingredients.Add(newIngredient);
            for (int i = 0; i < numOfIngredients - 1; i++)
            {
                Console.WriteLine("--------------------------------------------------------------------------");
                message = "What is the next ingredients name?";
                Console.WriteLine(message);
                Speak(message);
                newIngredient = new Ingredient();
                newIngredient.name = StringCheck();
                message = "What unit of measurement(tsp / tblsp / cups) will the ingredient use?";
                Console.WriteLine(message);
                Speak(message);
                newIngredient.unit = Console.ReadLine();
                message = "How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Meaasurement)";
                Console.WriteLine(message);
                Speak(message);
                while (newIngredient.quantity <= 0)
                {
                    try
                    {
                        newIngredient.quantity = Double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        message = $"{ex.Message}";
                        Console.WriteLine(message);
                        Speak(message);
                        message = "Please Type in a Number that is Greater than Zero. No Letters, Words or Phrases.";
                        Console.WriteLine(message);
                        Speak(message);
                        continue;
                    }
                }
                Ingredients.Add(newIngredient);
            }
        }

        public void InputStep()
        {
            NumOfSteps = 1;
            Console.WriteLine("------------------------------------------------------------------------------");
            message = "What is Step " + NumOfSteps;
            Console.WriteLine(message);
            Speak(message);
            Step newStep = new Step();
            newStep.StepDescription = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(newStep.StepDescription)) 
            {
                message = "The Step Cannot Be Empty. Please Try Again.";
                Console.WriteLine(message);
                Speak(message);
                newStep.StepDescription = Console.ReadLine();
            }
            Steps.Add(newStep);
            while (newStep.StepDescription.ToUpper() != "XXX")
            {
                NumOfSteps++;
                message = "What is Step " + NumOfSteps;
                Console.WriteLine(message);
                Speak(message);
                message = "If you are finish, you can type in finish to end.";
                Console.WriteLine(message);
                Speak(message);
                newStep = new Step();
                newStep.StepDescription = Console.ReadLine();
                while (String.IsNullOrWhiteSpace(newStep.StepDescription))
                {
                    message = "The Step Cannot Be Empty. Please Try Again.";
                    Console.WriteLine(message);
                    Speak(message);
                    newStep.StepDescription = Console.ReadLine();
                }
                Steps.Add(newStep);
            }
            Steps.RemoveAt(NumOfSteps - 1);
        }

        public void CreateRecipe()
        {
            message = "What is the name of your recipe?";
            Console.WriteLine(message);
            Speak(message);
            RecipeName = StringCheck();
            message = "How many different types of ingredients will you use? (For Example, if you have 3 Apples and 1 Banana then you have 2 ingredients.)";
            Console.WriteLine(message);
            Speak(message);
            while (NumOfIngredients == 0)
            {
                try
                {
                    NumOfIngredients = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Console.WriteLine(message);
                    Speak(message);
                    message = "Please Type in a Number. No Letters, Words or Phrases.";
                    Console.WriteLine(message);
                    Speak(message);
                    continue;
                }
            }
            InputIngredients(NumOfIngredients);
            InputStep();
            Console.WriteLine("------------------------------------------------------------------------------");
            Scale = 1;
            Recipes recipes = new Recipes(RecipeName, NumOfIngredients, Ingredients, Steps, NumOfSteps, Scale);
        }

        public string StringCheck()
        {
            string input = Console.ReadLine();
            while (Regex.IsMatch(input, @"\d") || String.IsNullOrEmpty(input))
            {
                if (Regex.IsMatch(input, @"\d"))
                {
                    message = "Please Do Not Enter Any Numbers";
                    Console.WriteLine(message);
                    Speak(message);

                }
                if (String.IsNullOrEmpty(input))
                {
                    message = "Please Do Not Leave Empty";
                    Console.WriteLine(message);
                    Speak(message);
                }
                message = "Please Try Again.";
                Console.WriteLine(message);
                Speak(message);
                input = Console.ReadLine();
            }
            return input;
        }

        public double GetScale()
        {
            message = "How would you like to scale the recipe? Please select from below." +
                "\nOption 1, Half. or Option 2, Double. or Option 3, Triple. ";
            Console.WriteLine(message);
            Speak(message);
            double scale = 1;
            string input;
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        scale = 0.5;
                        break;

                    case "2":
                        scale = 2;
                        break;

                    case "3":
                        scale = 3;
                        break;

                    default:
                        message = "Please select Either Option 1 for Half, 2 for Double or 3 for Triple.";
                        Console.WriteLine(message);
                        Speak(message);
                        continue;
                }

            } while (input != "1" && input != "2" && input != "3" && input != null);

            return scale;
        }

        public void ScaleRecipe(Recipes recipes, double scale)
        {
            Scale *= scale;
            foreach (var ingredient in Ingredients)
            {
                ingredient.quantity *= scale;
            }
        }

        public void ViewRecipe()
        {
            Console.WriteLine("\n\nRecipe: " +
               "\n***************************************************************" +
               "\n" + RecipeName + ": " +
               "\n---------------------------------------------------------------------" +
                "\nScale: " + Scale +
               "\n" + NumOfIngredients + " Ingredients" +
               "\n\n" + "Ingredients: " +
               "\n==============");
            foreach (var Ingredients in Ingredients)
            {
                Console.WriteLine(Ingredients.ToString());
            }
            Console.WriteLine("\nInstructions: " +
                "\n==============");
            int i = 1;
            foreach (var Step in Steps)
            {
                Console.WriteLine(i + ". " + Step.ToString());
                i++;
            }
            Console.Write("Finish!\nEnjoy!!!");
            Speak("Recipe:" + RecipeName + "Scale: " + Scale + NumOfIngredients + "Ingredients. Ingredients:");
            foreach (var Ingredients in Ingredients)
            {
                Speak(Ingredients.ToString());
            }
            Speak("\nInstructions: ");
            i = 1;
            foreach (var Step in Steps)
            {
                Speak("Step" + i + ". " + Step.ToString());
                i++;
            }
            Speak("Finish!\nEnjoy!!!");
           
        }

        public void ResetScale(Recipes recipes, double scale)
        {
            Scale = 1;
            foreach (var ingredient in Ingredients)
            {
                ingredient.quantity /= scale;
            }
        }

    }
}
