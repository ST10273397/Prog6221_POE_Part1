using Prog6221_POE;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for CARIngredient.xaml
    /// </summary>
    public partial class CARIngredient : Page
    {
        List<Ingredient> ingredientsList = new List<Ingredient>();
        DataStorecs ds = new DataStorecs();
//----------------------------------------------------------------------//

        public CARIngredient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for Continue button click.
        /// Navigates to CARSteps page.
        /// </summary>
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                NavigationService.Navigate(new CARSteps(ingredientsList, ds));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while navigating: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Event handler for Add Ingredient button click.
        /// Validates inputs, adds ingredient to the list, and updates total calories.
        /// </summary>
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Recipes recipes = new Recipes();
                Ingredient ingredient = new Ingredient();

                string recipeName = txtRecipeName.Text;
                string ingredientName = txtIngredientName.Text;
                double quantity;
                double calories;
                double totalCalories = double.Parse(txtblcTotalCalories.Text);

                // Check if recipe name already exists
                if (Recipes.RecipeList.ContainsKey(recipeName))
                {
                    MessageBox.Show("A recipe with this name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate recipe name
                if (!recipes.StringCheck(recipeName) || recipeName == "Recipe Name")
                {
                    MessageBox.Show("Please type in a valid recipe name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate ingredient name
                if (!recipes.StringCheck(ingredientName) || ingredientName == "Ingredient Name")
                {
                    MessageBox.Show("Please type in a valid ingredient name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cmboMeasurement.SelectedItem.ToString() == "Measurement")
                {
                    MessageBox.Show("Please select a valid measurement.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cmboFoodGroup.SelectedItem.ToString() == "Food Group")
                {
                    MessageBox.Show("Please select a valid Food Group.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ComboBoxItem measurementItem = (ComboBoxItem)cmboMeasurement.SelectedItem;
                string measurement = measurementItem.Content.ToString();

                ComboBoxItem foodGroupItem = (ComboBoxItem)cmboFoodGroup.SelectedItem;
                string foodGroup = foodGroupItem.Content.ToString();

                // Validate quantity
                if (!double.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity greater than 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate calories
                if (!double.TryParse(txtCalories.Text, out calories) || calories <= 0)
                {
                    MessageBox.Show("Please enter valid calories greater than 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update total calories and data store
                totalCalories += calories;
                txtblcTotalCalories.Text = totalCalories.ToString();
                ds.RecipeName = recipeName;
                ds.TotalCalories = totalCalories;

                // Create and add the ingredient to the list
                Ingredient newIngredient = new Ingredient(ingredientName, quantity, 1, measurement, txtOtherMeasurement.Text, calories, foodGroup);
                PopulateList(newIngredient);
                ClearUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the ingredient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to populate the list view with the new ingredient.
        /// </summary>
        private void PopulateList(Ingredient ing)
        {
            ingredientsList.Add(ing);
            lstIngredients.Items.Add(ing.ToString());
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to clear the UI inputs.
        /// </summary>
        private void ClearUI()
        {
            txtIngredientName.Clear();
            txtQuantity.Clear();
            cmboMeasurement.SelectedIndex = 0;
            txtCalories.Clear();
            cmboFoodGroup.SelectedIndex = 0;
            txtOtherMeasurement.Clear();
        }
    }
}
//--------------------------------------------------------------___4949499494949949494___END-OF-FILE___49494949494949494949___--------------------------------------------------------------------------------------------------//