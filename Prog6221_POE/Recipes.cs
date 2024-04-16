using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Prog6221_POE
{
    internal class Recipes
    {
        public string RecipeName {  get; set; }

        public int NumOfIngredients {  get; set; }

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
            Console.WriteLine("Please Enter the First Ingredient Name");
            string name = Console.ReadLine();
            Console.WriteLine("Please Enter the Unit of Measurement (whole/ml/cups/teaspoons)");
            string unit = Console.ReadLine();
            Console.WriteLine("How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Meaasurement)");
            double quantity = Double.Parse(Console.ReadLine());
            Ingredient newIngredient = new Ingredient { name = name, quantity = quantity, unit = unit, scale = 1 };
            Ingredients.Add(newIngredient);
            for (int i = 0; i < numOfIngredients - 1; i++)
            {
                Console.WriteLine("Enter the Next Ingredient");
                newIngredient = new Ingredient();
                newIngredient.name = Console.ReadLine();
                Console.WriteLine("Please Enter the Unit of Measurement (whole/ml/cups/teaspoons)");
                newIngredient.unit = Console.ReadLine();
                Console.WriteLine("How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Meaasurement)");
                newIngredient.quantity = Double.Parse(Console.ReadLine());
                Ingredients.Add(newIngredient);
            }
        }

        public void InputStep()
        {
            NumOfSteps = 1;
            Console.WriteLine("Please Enter Step " + NumOfSteps);
            Step newStep = new Step();
            newStep.StepDescription = Console.ReadLine();
            Steps.Add(newStep);
            while (newStep.StepDescription != "Finish")
            {
                NumOfSteps++;
                Console.WriteLine("Please Enter the Step " + NumOfSteps + " or Finish to Finish");
                newStep = new Step();
                newStep.StepDescription = Console.ReadLine();
                Steps.Add(newStep);
            }
        }

        public void CreateRecipe()
        {
            Console.WriteLine("Please Enter A Name For Your Recipe");
            RecipeName = Console.ReadLine();
            Console.WriteLine("Please Enter The Number of Ingredients For Your Recipe");
            NumOfIngredients = int.Parse(Console.ReadLine());
            InputIngredients(NumOfIngredients);
            InputStep();
            Scale = 1;
            Recipes recipes = new Recipes(RecipeName, NumOfIngredients, Ingredients, Steps, NumOfSteps, Scale);
        }

        public double GetScale()
        {
            Console.WriteLine("Please Select How You Want To Scale The Recipe");
            Console.WriteLine("1. Half or 2. Double or 3. Triple");
            double scale = 1;
                int input = int.Parse(Console.ReadLine());
            while (true)
            {
                switch (input)
                {
                    case 1:
                        scale = 0.5;
                        return scale;

                    case 2:
                        scale = 2;
                        return scale;

                    case 3:
                        scale = 3;
                        return scale;

                    default:
                        Console.WriteLine("Please select Either Option 1 for Half, 2 for Double or 3 for Triple.");
                        GetScale();
                        continue;
                }

            }
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
                "\n" + NumOfIngredients + " Ingredients" +
                "\nScale:" + Scale +
                "\n\n" + "Ingredients: ") ;
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
