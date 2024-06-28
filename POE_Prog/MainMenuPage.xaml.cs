using Prog6221_POE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        Dictionary<string, Recipes> recipeList; //Dictionary to store the recipes, mainly used to pass the recipe list to different pages where it is needed

        /// <summary>
        /// To initialize the page
        /// </summary>
        /// <param name="recipeList"></param>
        public MainMenu(Dictionary<string, Recipes> recipeList)
        {
            InitializeComponent();
            this.recipeList = recipeList;
        }

//-------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button to Navigate to the Create A Recipe (CAR) Process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCAR_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Create A Recipe (CAR) page
            NavigationService.Navigate(new CARIngredient());
        }

//-------------------------------------------------------------------------------------------//
        
        /// <summary>
        /// A Button to navigate to the View A Recipe (VAR) Page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVAR_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to View A Recipe (VAR) page, passing the recipeList dictionary
            NavigationService.Navigate(new VAR(recipeList));
        }

//-------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button to navigate to a Page that will Display the Recipes (DR)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDR_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Display Recipes page
            NavigationService.Navigate(new DisplayRecipesxaml());
        }

//-------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button to navigate to a Page to Search For A Recipe (SFAR)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSFAR_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Search For A Recipe page
            NavigationService.Navigate(new SearchRecipe());
        }

//-------------------------------------------------------------------------------------------------//

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
//--------------------------------------------------------------------------------___4994949494949494949494___END-OF-FILE___49949494949494949494_________________________________________________//