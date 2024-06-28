using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Media;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics.CodeAnalysis;
using System.Security.Policy;
using System.Linq;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace Prog6221_POE
{
    public class Recipes
    {
        //Properties to stroe recipe details
        public string RecipeName { get; set; }

        public int NumOfIngredients { get; set; }

        public static Dictionary<string, Recipes> RecipeList { get; set; } = new Dictionary<string, Recipes>();

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // List to store ingredients

        public List<Step> Steps { get; set; } = new List<Step>(); // List to store steps

        public int NumOfSteps { get; set; }

        public double Scale { get; set; }

        public double TotalCalories { get; set; }

        delegate string CalorieChecker(double calories);

        CalorieChecker Calories = new CalorieChecker(NotifyCalories);

        //Property to control text-to-speech feature
        public bool Talk { get; set; }

        //Message string for communication with the user
        public string message { get; set; }

        //SoundPlayer for playing music
        public SoundPlayer MusicMan = new SoundPlayer();

        public static string dark;

        //Default constructor
        public Recipes() { }

        /// <summary>
        /// Parameterized constructor to initialize recipe details
        /// </summary>
        /// <param name="recipeName"></param>
        /// <param name="numOfIngredients"></param>
        /// <param name="ingredients"></param>
        /// <param name="tcal"></param>
        /// <param name="steps"></param>
        /// <param name="stepNum"></param>
        /// <param name="scale"></param>
        public Recipes(string recipeName, int numOfIngredients, List<Ingredient> ingredients, double tcal, List<Step> steps, int stepNum, double scale)
        {
            RecipeName = recipeName;
            Ingredients = ingredients;
            NumOfIngredients = numOfIngredients;
            Steps = steps;
            NumOfSteps = stepNum;
            Scale = scale;
            TotalCalories = tcal;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------\\

        public async Task StopMusic()
        {
            // Array of songs
            string[] songs = { "Silence.wav" };

            // Use Task.Run to execute the music playing task asynchronously
            await Task.Run(() =>
            {
                while (true)
                {
                    // Playing songs indefinitely
                    foreach (string song in songs)
                    {
                        using (var MusicMan = new SoundPlayer(song))
                        {
                            MusicMan.Load();
                            MusicMan.PlaySync();
                        }
                    }
                }
            });
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\

        public async Task PlayMusicAsync()
        {
            // Array of songs
            string[] songs = { "Camille, Michael Giacchino - Le Festin (From ＂Ratatouille＂).wav", "Apotos (Day) - Sonic Unleashed [OST].wav", "Happy Day in Paris.wav" };

            // Use Task.Run to execute the music playing task asynchronously
            await Task.Run(() =>
            {
                while (true)
                {
                    // Playing songs indefinitely
                    foreach (string song in songs)
                    {
                        using (var MusicMan = new SoundPlayer(song))
                        {
                            MusicMan.Load();
                            MusicMan.PlaySync();
                        }
                    }
                }
            });
        }



        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\


        /// <summary>
        /// Method to check and ensure input is not empty or numeric
        /// </summary>
        /// <returns></returns>
        public bool StringCheck(string input)
        {
            //Checking if input contains numbers
            if (Regex.IsMatch(input, @"\d"))
            {
                message = "Please Do Not Enter Any Numbers. Please Try Again.";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Checking if input is empty
            if (String.IsNullOrEmpty(input))
            {
                message = "Please Do Not Leave Empty. Please Try Again.";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }

        //-------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// Method to get the scale for the recipe
        /// </summary>
        /// <returns></returns>
        public double GetScale()
        {
            //Prompting the user to select a scale option
            message = "How would you like to scale the recipe? Please select from below." +
                "\nType 1 for Half, Type 2 for Double or Type 3 for Triple. ";
            Console.WriteLine(message);
            double scale = 1;
            string input;

            //Looping until a valid input is provided
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        scale = 0.5;//Half Scale
                        break;

                    case "2":
                        scale = 2;//Double Scale
                        break;

                    case "3":
                        scale = 3;//Triple Scale
                        break;

                    default:
                        //Prompting the user to select a valid option
                        message = "Please type either 1 for Half, 2 for Double or 3 for Triple.";
                        Console.WriteLine(message);
                        continue;
                }

            } while (input != "1" && input != "2" && input != "3" && input != null);

            return scale;
        }

        //---------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// Method to scale the recipe ingredients based on the given factor
        /// </summary>
        /// <param name="scale"></param>
        public void ScaleRecipe(double scale, Recipes recipes)
        {
            //Constraits for conversion factors between different units
            const double TSP_PER_TBSP = 3.0;
            const double TBSP_PER_CUP = 16.0;
            const double TSP_PER_CUP = TSP_PER_TBSP * TBSP_PER_CUP;

            //Scaling the overall recipe scale
            Scale *= scale;

            //Scaling wach ingredient in the recipe
            foreach (var ingredient in recipes.Ingredients)
            {
                //Scaling the quantity of the ingredient
                ingredient.quantity *= scale;

                //Checking id the quantity exceeds conversion thresholds for tsp to tbsp and tbsp to cups
                if (ingredient.unit == "Tsps" && ingredient.quantity >= TSP_PER_TBSP)
                {
                    ingredient.quantity /= TSP_PER_TBSP; //Converting tsp to cups
                    ingredient.unit = "Tbsps"; //Updating unit
                }
                else if (ingredient.unit == "Tbsps" && ingredient.quantity >= TBSP_PER_CUP)
                {
                    ingredient.quantity /= TBSP_PER_CUP; //Converting tsp to cups
                    ingredient.unit = "Cups"; //Updating unit
                }
                else if (ingredient.unit == "Tsps" && ingredient.quantity >= TSP_PER_CUP)
                {
                    ingredient.quantity /= TSP_PER_CUP; //Converting tsp to cups
                    ingredient.unit = "Cups"; //Updating unit
                }
            }
            //Displaying the updated recipe after scaling
                  }

        //----------------------------------------------------------------------------------------------------------------------------------------\\

        //Method to display the recipe details and provide options for scaling, resetting or creating a new recipe
        public string ToString(Recipes recipes)
        {
            //Displaying recipe details
            string ing = "\n\nRecipe: " +
               "\n***************************************************************" +
               "\n" + recipes.RecipeName + ": " +
               "\n---------------------------------------------------------------------" +
                "\nScale: " + recipes.Scale +
               "\n" + recipes.NumOfIngredients + " Ingredients" +
               "\nCalories: " + TotalCalories +
               "\n\n" + "Ingredients: " +
               "\n==============";
            return ing;
        }
        //-------------------------------------------------------------------------------------------------------------------------------\\

        //Method to reset the scaling of the recipe
        public void ResetScale(Recipes recipes)
        {
            //Constants for conversion rates
            const double TSP_PER_TBSP = 3.0;
            const double TBSP_PER_CUP = 16.0;
            const double TSP_PER_CUP = TSP_PER_TBSP * TBSP_PER_CUP;

            //Calculating the inverse scale to reset the quantities
            double inverseScale = 1 / recipes.Scale;
            foreach (var ingredient in recipes.Ingredients)
            {
                //Resetting the quantity of each ingredient
                ingredient.quantity *= inverseScale;

                //Adjusting the unit nased on the quantity and conversion rates
                if (ingredient.unit == "Tbsps" && ingredient.quantity <= TSP_PER_TBSP)
                {
                    ingredient.quantity *= TSP_PER_TBSP;
                    ingredient.unit = "Tsps";
                }
                else if (ingredient.unit == "Cups" && ingredient.quantity <= TBSP_PER_CUP)
                {
                    ingredient.quantity *= TBSP_PER_CUP;
                    ingredient.unit = "Tbsps";
                }
                else if (ingredient.unit == "Cups" && ingredient.quantity <= TSP_PER_CUP)
                {
                    ingredient.quantity *= TSP_PER_CUP;
                    ingredient.unit = "Tsps";
                }
            }

            //Reseting the scale to 1
            Scale = 1;
        }

        //--------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// This Method searches for the recipe that the user is looking for then displays it.
        /// </summary>
        public void SearchRecipe()
        {
            // Asking for the name of the recipe to search for
            Console.WriteLine("Enter the name of the recipe you want to search for:");
            string searchName = Console.ReadLine();

            // Check if the recipe exists in the RecipeList dictionary
            if (RecipeList.ContainsKey(searchName))
            {
                // If found, display the recipe details
                Console.WriteLine("Recipe found!");
 // Assuming ViewRecipe() method exists to display recipe details
            }
            else
            {
                // If not found, display a message
                Console.WriteLine("Recipe not found. Please check the spelling or try another name.");
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// This method will displays all the recipes in alphabetical order (hopefully).
        /// </summary>
        public void DisplayRecipes()
        {
            Console.WriteLine("Recipes\n*********************************");
            foreach (var recipeName in RecipeList.Keys)
            {
                Console.WriteLine(recipeName + "\n");;
            }
            SearchRecipe(); //Allows the user to look for a specific recipe after
        }
        //--------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// Notifies the user when they reached over 300 calories
        /// </summary>
        /// <param name="calories"></param>
        /// <returns></returns>
        public static string NotifyCalories(double calories)
        {
            string notification;

            if (calories >= 300)
            {
                notification = "THIS RECIPE HAS REACHED 300 CALORIES!! YOU SHOULD RE-EVALUATE YOUR RECIPE IF YOU WANT LESS THAN 300 CALORIES!\nYou have " + calories + " calories in your recipe so far";
                return notification;
            }
            else
            {
                if (calories >= 200)
                {
                    notification = "YOU ARE CLOSE TO 300 CALORIES!! YOU SHOULD RE-EVALUATE YOUR RECIPE IF YOU WANT LESS THAN 300 CALORIES!\nYou have " + calories + " calories in your recipe so far";
                    return notification;
                }
            }
            notification = "You have " + calories + " calories in your recipe so far";
            return notification;
        }
    }
}
//----------------------------------------------------------------------4949449494949494___END-OF-FILE___494994949494944949----------------------------------------------------------------------------------------------------\\