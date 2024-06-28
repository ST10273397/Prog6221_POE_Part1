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
using System.Windows.Shapes;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, Recipes> recipeList = new Dictionary<string, Recipes>(); //A Dictionary to store the recipe list, mainly used to pass the recipe list to the Main Menu Page

        /// <summary>
        /// To initialize the window and the page
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

//------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button to exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // Exit the application when the Exit button is clicked
            System.Environment.Exit(0);
        }

//--------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button to start the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the MainMenu page when the Start button is clicked
            startFrame.Navigate(new MainMenu(recipeList));
        }
    }
}