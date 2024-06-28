using System;

namespace POE_Prog
{
    //A class to store details that fall between the Ingredients and Steps
    public class DataStorecs
    {
        public string RecipeName { get; set; } //To store the recipe
        public double TotalCalories { get; set; } //To store the calories

        public DataStorecs() { }

        public DataStorecs(string rName, double tCal)
        {
            RecipeName = rName;
            TotalCalories = tCal;
        }
    }
}