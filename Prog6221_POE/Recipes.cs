using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;
using System.Media;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Prog6221_POE
{
    internal class Recipes
    {
        //Properties to stroe recipe details
        public string RecipeName { get; set; }

        public int NumOfIngredients { get; set; }

        public List<Recipes> RecipeList { get; set; } = new List<Recipes>();

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // List to store ingredients

        public List<Step> Steps { get; set; } = new List<Step>(); // List to store steps

        public int NumOfSteps { get; set; }

        public double Scale { get; set; }

        public double TotalCalories { get; set; }

        delegate void CalorieChecker();

        //SpeechSynthesizer for text-to-speech functionality
        SpeechSynthesizer talk = new SpeechSynthesizer();

        //Property to control text-to-speech feature
        public bool Talk { get; set; }

        //Message string for communication with the user
        public string message { get; set; }

        public string dark { get; set; }

        //SoundPlayer for playing music
        public System.Media.SoundPlayer MusicMan = new SoundPlayer();

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

        //Method to start the recipe creation process
        public void Start()
        {
            Settings();
            ViewRecipe(CreateRecipe());
        }

        //Method to configure user settings
        public void Settings()
        {
            //User prefernces for text-to-speech
            Console.WriteLine("Would you like text-to-speech? (yes/no)");
            talk.Speak("Would you like text-to-speech? (yes/no)");
            string answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                talk.Speak(message);
                answer = StringCheck();
            }
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


            //User prefernce for dark or light mode
            message = "Would you like dark or light mode? Please type in only 'dark' or 'light'.";
            Console.WriteLine(message);
            Speak(message);
            dark = StringCheck();
            while (dark.ToLower() != "light" && dark.ToLower() != "dark")
            {
                message = "Please only type in Light or Dark";
                Console.WriteLine(message);
                talk.Speak(message);
                dark = StringCheck();
            }
            if (dark.ToLower() == "light")
            {
                
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            }

            //Prompting user for usic prefernce
            message = "Would you like music? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please type in only 'yes' or 'no'.";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }
            if (answer.ToLower() == "yes")
            {
                PlayMusicAsync();
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

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
        /// Method to speak a message if text-to-speech is enabled
        /// </summary>
        /// <param name="message"></param>
        public void Speak(string message)
        {
            //Checking if text-to-speech is enabled
            if (Talk)
            {
                //Speaking the message using the SpeechSynthesizer
                talk.Speak(message);
            }
        }

        //---------------------------------------------------------------------------\\

        /// <summary>
        /// Method to input ingredeints for the recipe
        /// </summary>
        /// <param name="numOfIngredients"></param>
        public void InputIngredients(int numOfIngredients)
        {
            string name = null;

            //Prompting user for the first ingredient's name
            Console.WriteLine("------------------------------------------------------------------------------");
            message = "What is the first ingredient's name?";
            Console.WriteLine(message);
            Speak(message);
            name = StringCheck();

            //Prompting tuser for the quantity of the ingredient
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

            //Prompting user for the unit of measurement of the ingredient
            message = "What unit of measurement will the ingredient use?\nType 1 for tsp, Type 2 for tbsp, Type 3 for cups or Type 4 for other.";
            Console.WriteLine(message);
            Speak(message);
            string input;
            Ingredient.Unit unit = Ingredient.Unit.other;
            string other = "";
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        unit = Ingredient.Unit.tsp;
                        break;

                    case "2":
                        unit = Ingredient.Unit.tbsp;
                        break;

                    case "3":
                        unit = Ingredient.Unit.cups;
                        
                        break;

                    case "4":
                        message = "What unit of measurement would you like to use?";
                        Console.WriteLine(message);
                        Speak(message);
                        unit = Ingredient.Unit.other;
                        other = Console.ReadLine();
                        break;

                    default:
                        message = "Please only Type 1 for tsp, Type 2 for tbsp, Type 3 for cups or Type 4 for other.";
                        Console.WriteLine(message);
                        Speak(message);
                        continue;
                }

            } while (input != "1" && input != "2" && input != "3" && input != "4" && input != null);

            message = "How many calories does this ingredient have?";
            Console.WriteLine(message);
            Speak(message);
            double calories = 0;
            while (calories <= 0)
            {
                try
                {
                    calories = Double.Parse(Console.ReadLine());
                    TotalCalories += calories;
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

            message = "What food group does this ingredient belong to?";
            Console.WriteLine(message);
            Speak(message);
            string foodGroup = StringCheck();

            //Creating a new Ingredient object with user inputs
            Ingredient newIngredient = new Ingredient { name = name, quantity = quantity, unit = unit, otherUnit = other, scale = 1, calories = calories, foodGroup = foodGroup };

            //Adding the new ingredient to the list of ingredients
            Ingredients.Add(newIngredient);

            //Looping to input the remaining ingredients
            for (int i = 0; i < numOfIngredients - 1; i++)
            {
                Console.WriteLine("--------------------------------------------------------------------------");
                message = "What is the next ingredients name?";
                Console.WriteLine(message);
                Speak(message);

                //Propting user for the name of the next ingredient
                newIngredient = new Ingredient();
                newIngredient.name = StringCheck();

                //Prompting user for the quantity of the next ingredient
                message = "How Much/Many of the Ingredient Will You Use? (Don't type in the Unit of Measurement)";
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

                //Prompting user for the unit of measurement of the next ingredient
                message = "What unit of measurement will the ingredient use?\nType 1 for tsp, Type 2 for tbsp, Type 3 for cups or Type 4 for other.";
                Console.WriteLine(message);
                Speak(message);
                do
                {
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            newIngredient.unit = Ingredient.Unit.tsp;
                            break;

                        case "2":
                            newIngredient.unit = Ingredient.Unit.tbsp;
                            break;

                        case "3":
                            newIngredient.unit = Ingredient.Unit.cups;
                            break;

                        case "4":
                            message = "What unit of measurement would you like to use?";
                            Console.WriteLine(message);
                            Speak(message);
                            newIngredient.unit = Ingredient.Unit.other;
                            newIngredient.otherUnit = Console.ReadLine();
                            break;

                        default:
                            message = "Please only Type 1 for tsp, Type 2 for tbsp, Type 3 for cups or Type 4 for other.";
                            Console.WriteLine(message);
                            Speak(message);
                            continue;
                    }

                } while (input != "1" && input != "2" && input != "3" && input != "4" && input != null);

                message = "How many calories does this ingredient have?";
                Console.WriteLine(message);
                Speak(message);
                newIngredient.calories = 0;
                while (calories <= 0)
                {
                    try
                    {
                        newIngredient.calories = Double.Parse(Console.ReadLine());
                        TotalCalories += newIngredient.calories;
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

                message = "What food group does this ingredient belong to?";
                Console.WriteLine(message);
                Speak(message);
                newIngredient.foodGroup = StringCheck();

                //Adding the new ingredient to the list of ingredients
                Ingredients.Add(newIngredient);
            }
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------\\

        //Method to input steps for the recipe
        public void InputStep()
        {
            NumOfSteps = 1;

            //Prompting user for the description of the first step
            Console.WriteLine("------------------------------------------------------------------------------");
            message = "What is Step " + NumOfSteps + "?";
            Console.WriteLine(message);
            Speak(message);
            Step newStep = new Step();
            newStep.StepDescription = Console.ReadLine();

            //Validating that the step description is not empty
            while (String.IsNullOrWhiteSpace(newStep.StepDescription))
            {
                message = "The Step Cannot Be Empty. Please Try Again.";
                Console.WriteLine(message);
                Speak(message);
                newStep.StepDescription = Console.ReadLine();
            }

            //Adding the first step to the list of steps
            Steps.Add(newStep);

            //Looping to input subsequent steps until "XXX" is entered
            while (newStep.StepDescription.ToUpper() != "XXX")
            {
                NumOfSteps++;
                message = "What is Step " + NumOfSteps + "?";
                Console.WriteLine(message);
                Speak(message);
                message = "Type XXX to Finish.";
                Console.WriteLine(message);
                Speak(message);
                newStep = new Step();
                newStep.StepDescription = Console.ReadLine();

                //Validating that the step description is not empty
                while (String.IsNullOrWhiteSpace(newStep.StepDescription))
                {
                    message = "The Step Cannot Be Empty. Please Try Again.";
                    Console.WriteLine(message);
                    Speak(message);
                    newStep.StepDescription = Console.ReadLine();
                }

                //Adding the step to the list of steps
                Steps.Add(newStep);
            }

            //Removing the last step, as it's the termination marker "XXX"
            Steps.RemoveAt(NumOfSteps - 1);
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------\\

        //Method to create a new recipe
        public Recipes CreateRecipe()
        {

            //Asking for the name of the recipe
            message = "What is the name of your recipe?";
            Console.WriteLine(message);
            Speak(message);
            RecipeName = StringCheck();

            //Asking for the number of ingredients
            message = "How many different types of ingredients will you use? (For Example, if your recipe requires 3 Apples and 1 Banana then you will use 2 ingredients.)";
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

            //Inputting ingredients
            InputIngredients(NumOfIngredients);

            //Inputting steps
            InputStep();

            //Displaying separator
            Console.WriteLine("------------------------------------------------------------------------------");

            //Setting the initial scale
            Scale = 1;

            //Creating a new recipe object
            Recipes recipes = new Recipes(RecipeName, NumOfIngredients, Ingredients, Steps, NumOfSteps, Scale);

            //Adding the recipe to the recipe list
            RecipeList.Add(recipes);

            //Sorting the Recipe List
            RecipeList.Sort();

            return recipes;
        }

//---------------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// Method to check and ensure input is not empty or numeric
        /// </summary>
        /// <returns></returns>
        public string StringCheck()
        {
            string input = Console.ReadLine();

            //Looping unitl valid input is provided
            while (Regex.IsMatch(input, @"\d") || String.IsNullOrEmpty(input))
            {

                //Checking if input contains numbers
                if (Regex.IsMatch(input, @"\d"))
                {
                    message = "Please Do Not Enter Any Numbers";
                    Console.WriteLine(message);
                    Speak(message);
                }

                //Checking if input is empty
                if (String.IsNullOrEmpty(input))
                {
                    message = "Please Do Not Leave Empty";
                    Console.WriteLine(message);
                    Speak(message);
                }

                //Asking for input again
                message = "Please Try Again.";
                Console.WriteLine(message);
                Speak(message);
                input = Console.ReadLine();
            }
            return input;
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
            Speak(message);
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
                        Speak(message);
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
                if (ingredient.unit == Ingredient.Unit.tsp && ingredient.quantity >= TSP_PER_TBSP)
                {
                    ingredient.quantity /= TSP_PER_TBSP; //Converting tsp to cups
                    ingredient.unit = Ingredient.Unit.tbsp; //Updating unit
                }
                else if (ingredient.unit == Ingredient.Unit.tbsp && ingredient.quantity >= TBSP_PER_CUP)
                {
                    ingredient.quantity /= TBSP_PER_CUP; //Converting tsp to cups
                    ingredient.unit = Ingredient.Unit.cups; //Updating unit
                }
                else if (ingredient.unit == Ingredient.Unit.tsp && ingredient.quantity >= TSP_PER_CUP)
                {
                    ingredient.quantity /= TSP_PER_CUP; //Converting tsp to cups
                    ingredient.unit = Ingredient.Unit.cups; //Updating unit
                }
            }
            //Displaying the updated recipe after scaling
            ViewRecipe(recipes);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------\\

        //Method to display the recipe details and provide options for scaling, resetting or creating a new recipe
        public void ViewRecipe(Recipes recipes)
        {
            //Displaying recipe details
            Console.WriteLine("\n\nRecipe: " +
               "\n***************************************************************" +
               "\n" + recipes.RecipeName + ": " +
               "\n---------------------------------------------------------------------" +
                "\nScale: " + recipes.Scale +
               "\n" + recipes.NumOfIngredients + " Ingredients" +
               "\n\n" + "Ingredients: " +
               "\n==============");
            foreach (var Ingredients in recipes.Ingredients)
            {
                Console.WriteLine(Ingredients.ToString());
            }
            Console.WriteLine("\nInstructions: " +
                "\n==============");
            int i = 1;
            foreach (var Step in recipes.Steps)
            {
                Console.WriteLine(i + ". " + Step.ToString());
                i++;
            }
            Console.Write("Finish!\nEnjoy!!!");

            //Speaking the recipe details
            Speak("Recipe:" + recipes.RecipeName + "Scale: " + recipes.Scale + ". " + recipes.NumOfIngredients + "Ingredients. Ingredients:");
            foreach (var Ingredients in recipes.Ingredients)
            {
                Speak(Ingredients.ToString());
            }
            Speak("\nInstructions: ");
            i = 1;
            foreach (var Step in recipes.Steps)
            {
                Speak("Step" + i + ". " + Step.ToString());
                i++;
            }
            Speak("Finish!\nEnjoy!!!");

            //Asking the user if they want to scale the recipe
            message = "\nWould you like to scale your recipe? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            string answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }
            if (answer.ToLower() == "yes")
            {
                double scale = GetScale();
                ScaleRecipe(scale, recipes);
            }

            //Asking the user if they want to reset the scale
            message = "Would you like to reset the scales? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }
            if (answer.ToLower() == "yes")
            {
                ResetScale(recipes);
            }

            //Asking the user if they want to rest the recipe
            message = "Would you like to reset the recipe? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }
            if (answer.ToLower() == "yes")
            {
                ResetRecipe(recipes);
            }

            //Asking the user if they want to create a new recipe
            message = "Would you like to create a new recipe? (Yes/No)";
            Console.WriteLine(message);
            Speak(message);
            answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }
            if (answer == "yes")
            {
                CreateRecipe();
                ViewRecipe(recipes);
            }
            else
            {
                //Exiting the application
                message = "Thank you for using the app! Press Enter to Exit.";
                Console.WriteLine(message);
                Speak(message);
                Console.ReadKey();
                Environment.Exit(0);
            }

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
                if (ingredient.unit == Ingredient.Unit.tbsp && ingredient.quantity <= TSP_PER_TBSP)
                {
                    ingredient.quantity *= TSP_PER_TBSP;
                    ingredient.unit = Ingredient.Unit.tsp;
                }
                else if (ingredient.unit == Ingredient.Unit.cups && ingredient.quantity <= TBSP_PER_CUP)
                {
                    ingredient.quantity *= TBSP_PER_CUP;
                    ingredient.unit = Ingredient.Unit.tbsp;
                }
                else if (ingredient.unit == Ingredient.Unit.cups && ingredient.quantity <= TSP_PER_CUP)
                {
                    ingredient.quantity *= TSP_PER_CUP;
                    ingredient.unit = Ingredient.Unit.tsp;
                }
            }

            //Reseting the scale to 1
            Scale = 1;

            //Displaying the updated recipe
            ViewRecipe(recipes);
        }

//--------------------------------------------------------------------------------------------------------------------------------\\

        //Method to reset the entire recipe
        public void ResetRecipe(Recipes recipe)
        {

            //Asking for confirmation before resetting
            message = "Are you sure? (yes/no)";
            Console.WriteLine(message);
            Speak(message);
            string answer = StringCheck();
            while (answer.ToLower() != "yes" && answer.ToLower() != "no")
            {
                message = "Please only type in Yes or No";
                Console.WriteLine(message);
                Speak(message);
                answer = StringCheck();
            }

            //Resetting the recipe if confirmed
            if (answer == "yes")
            {
                //Resetting recipe properties
                recipe.RecipeName = "";
                recipe.Ingredients.Clear();
                recipe.NumOfIngredients = 0;
                recipe.Steps.Clear();
                recipe.Scale = 0;
                recipe.NumOfSteps = 0;

                //Creating a new instance of Recipes class
                Recipes recipes = new Recipes(RecipeName, NumOfIngredients, Ingredients, Steps, 0, 0);
            }

            //Displaying the updated (empty) recipe
            ViewRecipe(recipe);
        }
//--------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// This Method searches for the recipe that the user is looking for then displays it.
        /// </summary>
        public void SearchRecipe()
        {
            string answer = StringCheck();
            foreach (var Recipes in RecipeList)
            {
                if (Recipes.RecipeName == answer)
                {
                    ViewRecipe(Recipes);
                    break;
                }
                else
                {
                    message = "There is no recipe with the name " + answer;
                    Console.WriteLine(message);
                    Speak(message);
                }
            }

        }
        //--------------------------------------------------------------------------------------------------------------------------------\\

        /// <summary>
        /// This method will displays all the recipes in alphabetical order (hopefully).
        /// </summary>
        public void DisplayRecipes()
        {
            Console.WriteLine("Recipes\n*********************************");
            Speak("Recipes");
            foreach (var Recipe in RecipeList)
            {
                Console.WriteLine(Recipe.RecipeName += "\n");
                Speak(Recipe.RecipeName);
            }
        }
//--------------------------------------------------------------------------------------------------------------------------------\\

        public void CheckcCalories()
        {
            if (TotalCalories > 300)
            {
                message = "THIS RECIPE HAS REACHED 300 CALORIES!! YOU SHOULD RE-EVALUATE YOUR RECIPE IF YOU WANT LESS THAN 300 CALORIES";
                if (dark == "dark")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(message);
                    Speak(message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Speak(message);
                }
            }
        }

    }
}
//----------------------------------------------------------------------4949449494949494___END-OF-FILE___494994949494944949----------------------------------------------------------------------------------------------------\\