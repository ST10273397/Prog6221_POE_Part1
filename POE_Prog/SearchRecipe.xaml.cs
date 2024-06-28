using Prog6221_POE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for SearchRecipe.xaml
    /// </summary>
    public partial class SearchRecipe : Page
    {
        // ObservableCollection to hold search results for binding to ListBox
        private ObservableCollection<Recipes> searchResults = new ObservableCollection<Recipes>();

        public SearchRecipe()
        {
            InitializeComponent();
            lstSearchResults.ItemsSource = searchResults; // Bind ObservableCollection to ListBox
        }

        /// <summary>
        /// Event handler for the Search button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtRecipeName.Text.Trim(); // Get the search term from the TextBox

            // Validate search term
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a recipe name to search.", "Search Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Exit the method if search term is empty
            }

            // Perform case-insensitive search in RecipeList for recipes containing the search term
            var results = Recipes.RecipeList.Values
                          .Where(recipe => recipe.RecipeName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                          .ToList();

            // Clear previous search results from ObservableCollection
            searchResults.Clear();

            // Add new search results to ObservableCollection
            foreach (var result in results)
            {
                searchResults.Add(result);
            }

            // If no results found, notify the user
            if (searchResults.Count == 0)
            {
                MessageBox.Show($"No recipes found with the name '{searchTerm}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
//-----------------------------------------------------------------------------___49494994949494___END-OF-FILE___494994949494949494--------------------------------------------------------------------------------------------------//