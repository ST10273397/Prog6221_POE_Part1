using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog6221_POE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Recipes recipes = new Recipes();
            Ingredient ingredient = new Ingredient();
            recipes.CreateRecipe();
            double scale = recipes.GetScale();
            recipes.ScaleRecipe(recipes, scale);
            recipes.ViewRecipe();
            recipes.ResetScale(recipes, scale);
            recipes.ViewRecipe();
            Console.ReadKey();
        }
    }
}
