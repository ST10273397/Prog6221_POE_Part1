using POE_Prog;
using Prog6221_POE;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Linq;

namespace POE_Prog
{
    public partial class CARSteps : Page
    {
        //The neccessary attributes for the Page
        private List<Ingredient> ingredients;
        private List<Step> steps;
        private string recipeName;
        private int numOfIngredients;
        private double totalCalories;
        private DataStorecs ds;

//------------------------------------------------------------------------------------------//

        /// <summary>
        /// Initializing the Page
        /// </summary>
        /// <param name="ingredients"></param>
        /// <param name="ds"></param>
        public CARSteps(List<Ingredient> ingredients, DataStorecs ds)
        {
            InitializeComponent();
            steps = new List<Step>();
            this.ingredients = ingredients;
            this.ds = ds;
        }

//------------------------------------------------------------------------------------------//

        /// <summary>
        /// A button for when the user is done adding their steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Recipes object and add it to the RecipeList
            recipeName = ds.RecipeName;
            numOfIngredients = ingredients.Count;
            totalCalories = ds.TotalCalories;

            Recipes recipe = new Recipes(recipeName, numOfIngredients, ingredients, totalCalories, steps, steps.Count, 1);
            Recipes.RecipeList.Add(recipeName, recipe);
            Recipes.RecipeList = Recipes.RecipeList.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            // Navigate to the MainMenu page and show a success message
            NavigationService.Navigate(new MainMenu(Recipes.RecipeList));
            MessageBox.Show("Recipe added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

//------------------------------------------------------------------------------------------//

        /// <summary>
        /// A button to Add Steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Step object and add it to the steps list
            Step step;
            string stepDescription = txtSteps.Text.Trim();
            int stepNumber = lstSteps.Items.Count + 1;

            if (string.IsNullOrEmpty(stepDescription) || stepDescription == "Enter A Step Here...")
            {
                MessageBox.Show("Please enter a valid step description.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            step = new Step(stepNumber, stepDescription);
            steps.Add(step);
            PopulateList(step);
            txtSteps.Clear();
        }

//-------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Method to populate the list
        /// </summary>
        /// <param name="step"></param>
        private void PopulateList(Step step)
        {
            // Add the step description to the ListBox
            lstSteps.Items.Add(step.StepDescription);
        }

//------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Metthod to Get the Food Group Data
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, double> GetFoodGroupData()
        {
            // Calculate and return food group data based on ingredients
            Dictionary<string, double> foodGroupData = new Dictionary<string, double>();

            foreach (var ingredient in ingredients)
            {
                if (foodGroupData.ContainsKey(ingredient.foodGroup))
                {
                    foodGroupData[ingredient.foodGroup] += ingredient.quantity;
                }
                else
                {
                    foodGroupData[ingredient.foodGroup] = ingredient.quantity;
                }
            }

            return foodGroupData;
        }
    }
}
//---------------------------------------------------------------------------------------___494949494949449494949___END-OF-FILE___494949494949949494949___-----------------------------------------------------//