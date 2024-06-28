using Prog6221_POE;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for DisplayRecipesxaml.xaml
    /// </summary>
    public partial class DisplayRecipesxaml : Page
    {
        //An observable collection to store the recipe list
        private ObservableCollection<Recipes> recipesList = new ObservableCollection<Recipes>();

        //To initialize the page to show the list of recipes
        public DisplayRecipesxaml()
        {
            InitializeComponent();
            lstRecipes.ItemsSource = recipesList;

            // Populate recipesList with data from Recipes.RecipeList
            foreach (var recipe in Recipes.RecipeList.Values)
            {
                recipesList.Add(recipe);
            }
        }
    }
}
//-----------------------------------------------------------___4949494494949494949___END-OF-FILE___49494949494944994949494944949___--------------------------------------------------------//