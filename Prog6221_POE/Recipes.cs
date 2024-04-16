using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public void InputIngredients(int numOfIngredients)
        {
            string name = null;
            Console.WriteLine("Please Enter the First Ingredient Name");
            name = StringCheck();
            Console.WriteLine("Please Enter the Unit of Measurement (whole/ml/cups/teaspoons)");
            string unit = Console.ReadLine();
            Console.WriteLine("How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Meaasurement)");
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
                    Console.WriteLine("Please Type in a Number. No Letters, Words or Phrases.");
                    continue;
                }
            }
            Ingredient newIngredient = new Ingredient { name = name, quantity = quantity, unit = unit, scale = 1 };
            Ingredients.Add(newIngredient);
            for (int i = 0; i < numOfIngredients - 1; i++)
            {
                Console.WriteLine("Enter the Next Ingredient");
                newIngredient = new Ingredient();
                newIngredient.name = StringCheck();
                Console.WriteLine("Please Enter the Unit of Measurement (whole/ml/cups/teaspoons)");
                newIngredient.unit = Console.ReadLine();
                Console.WriteLine("How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Meaasurement)");
                while (newIngredient.quantity <= 0)
                {
                    try
                    {
                        newIngredient.quantity = Double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        Console.WriteLine("Please Type in a Number that is Greater than Zero. No Letters, Words or Phrases.");
                        continue;
                    }
                }
                Ingredients.Add(newIngredient);
            }
        }

        public void InputStep()
        {
            NumOfSteps = 1;
            Console.WriteLine("Please Enter Step " + NumOfSteps);
            Step newStep = new Step();
            newStep.StepDescription = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(newStep.StepDescription)) 
            {
                Console.WriteLine("The Step Cannot Be Empty");
                Console.WriteLine("Please Try Again");
                newStep.StepDescription = Console.ReadLine();
            }
            Steps.Add(newStep);
            while (newStep.StepDescription != "Finish")
            {
                NumOfSteps++;
                Console.WriteLine("Please Enter the Step " + NumOfSteps + " or Finish to Finish");
                newStep = new Step();
                newStep.StepDescription = Console.ReadLine();
                while (String.IsNullOrWhiteSpace(newStep.StepDescription))
                {
                    Console.WriteLine("The Step Cannot Be Empty");
                    Console.WriteLine("Please Try Again");
                    newStep.StepDescription = Console.ReadLine();
                }
                Steps.Add(newStep);
            }
        }

        public void CreateRecipe()
        {
            Console.WriteLine("Please Enter A Name For Your Recipe");
            RecipeName = StringCheck();
            Console.WriteLine("Please Enter The Number of Ingredients For Your Recipe");
            while (NumOfIngredients == 0)
            {
                try
                {
                    NumOfIngredients = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please Type in a Number. No Letters, Words or Phrases.");
                    continue;
                }
            }
            InputIngredients(NumOfIngredients);
            InputStep();
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
                    Console.WriteLine("Please Do Not Enter Any Numbers");

                }
                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please Do Not Leave Empty");
                }
                Console.WriteLine("Please Try Again.");
                input = Console.ReadLine();
            }
            return input;
        }

        public double GetScale()
        {
            Console.WriteLine("Please Select How You Want To Scale The Recipe");
            Console.WriteLine("1. Half or 2. Double or 3. Triple");
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
                        Console.WriteLine("Please select Either Option 1 for Half, 2 for Double or 3 for Triple.");
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
            Console.WriteLine("\nRecipe: " +
                "\n" + RecipeName + ": " +
                 "\nScale:" + Scale +
                "\n" + NumOfIngredients + " Ingredients" +
                "\n\n" + "Ingredients: ");
            foreach (var Ingredients in Ingredients)
            {
                Console.WriteLine(Ingredients.ToString());
            }
            Console.WriteLine(" \n Instructions: ");
            int i = 1;
            foreach (var Step in Steps)
            {
                Console.WriteLine(i + ". " + Step.ToString());
                i++;
            }
            Console.Write("Enjoy!");
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
