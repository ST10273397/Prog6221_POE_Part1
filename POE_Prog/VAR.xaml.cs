using Prog6221_POE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POE_Prog
{
    public partial class VAR : Page
    {
        private Dictionary<string, Recipes> recipeList; //A Dictionary to save the recipes in a list

        public VAR(Dictionary<string, Recipes> recipeList)
        {
            InitializeComponent();
            this.recipeList = recipeList;

            // Populate ComboBox with recipe names
            cmboRecipes.ItemsSource = recipeList.Keys;
        }

//------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Event handler for View Recipe button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (cmboRecipes.SelectedItem != null)
            {
                string key = cmboRecipes.SelectedItem.ToString();

                // Check if the selected recipe exists in RecipeList
                if (Recipes.RecipeList.ContainsKey(key))
                {
                    var selectedRecipe = Recipes.RecipeList[key];

                    // Construct recipe details string
                    var recipeDetails = $"Recipe: {selectedRecipe.RecipeName}\n\nIngredients:\n";
                    int i = 1;
                    foreach (var ingredient in selectedRecipe.Ingredients)
                    {
                        recipeDetails += $"{i}. {ingredient.name} - {ingredient.quantity} {ingredient.unit}\n Calories: {ingredient.calories},\n Food Group: {ingredient.foodGroup}\n";
                        i++;
                    }
                    recipeDetails += $"Total Calories: {selectedRecipe.TotalCalories}\nSteps:\n";
                    i = 1;
                    foreach (var step in selectedRecipe.Steps)
                    {
                        recipeDetails += $"{i}. {step}\n";
                        i++;
                    }

                    // Display recipe details in the text block
                    txtblcRecipe.Text = recipeDetails;
                }
                else
                {
                    // Notify user if the selected recipe is not found
                    MessageBox.Show($"Recipe '{key}' not found.", "Recipe Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Notify user if no recipe is selected
                MessageBox.Show("Please select a recipe to view.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

//------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Event handler for View Pie Chart button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewPieChart_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (cmboRecipes.SelectedItem != null)
            {
                string key = cmboRecipes.SelectedItem.ToString();

                // Check if the selected recipe exists in RecipeList
                if (Recipes.RecipeList.ContainsKey(key))
                {
                    var selectedRecipe = Recipes.RecipeList[key];

                    // Calculate calorie distribution by food group
                    Dictionary<string, double> foodGroupData = new Dictionary<string, double>();
                    foreach (var ingredient in selectedRecipe.Ingredients)
                    {
                        if (foodGroupData.ContainsKey(ingredient.foodGroup))
                        {
                            foodGroupData[ingredient.foodGroup] += ingredient.calories;
                        }
                        else
                        {
                            foodGroupData[ingredient.foodGroup] = ingredient.calories;
                        }
                    }

                    // Navigate to PieChart page with food group data
                    NavigationService.Navigate(new PieChart(foodGroupData));
                }
                else
                {
                    // Notify user if the selected recipe is not found
                    MessageBox.Show($"Recipe '{key}' not found.", "Recipe Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Notify user if no recipe is selected
                MessageBox.Show("Please select a recipe to view.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
//--------------------------------------------------------------------___499494494949494949494___END-OF-FILE___4994949494949494949___-------------------------------------------------//