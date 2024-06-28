using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Prog6221_POE
{
    public class Ingredient
    {
        //Properties to store ingredient name, quantity, scale, unit, and other unit.

        public string recipeName {  get; set; }
        public string name { get; set; }

        public double quantity { get; set; }

        public double scale { get; set; }

        public string unit { get; set; }

        public string otherUnit { get; set; }

        public double calories { get; set; }

        public string foodGroup { get; set; }

        //Default Constructor
        public Ingredient() { }


        /// <summary>
        /// Constructor with parameters to initialize ingredient properties
        /// </summary>
        /// <param name="nme"></param>
        /// <param name="qnty"></param>
        /// <param name="scle"></param>
        /// <param name="u"></param>
        /// <param name="oUnit"></param>
        /// <param name="cal"></param>
        /// <param name="fgrp"></param>
        public Ingredient(string nme, double qnty, double scle, string u, string oUnit, double cal, string fgrp)
        {
            name = nme;
            quantity = qnty;
            scale = scle;
            unit = u;
            otherUnit = oUnit;
            calories = cal;
            foodGroup = fgrp;
        }

        //-----------------------------------------------------------------------------------------\\

        /// <summary>
        /// Method to scale the quantity of the ingredient
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public double Scale(double scale)
        {
            this.quantity *= scale;
            return scale;
        }

        //------------------------------------------------------------------------------------------\\

        /// <summary>
        /// Override ToString() method to return a string representation of the ingredient
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Adjust this method based on how you want to display each ingredient
            if (unit == "Other")
            {
                return $"{name}, {quantity} {otherUnit}\nCalories: {calories}\nFood Group: {foodGroup}";
            }
            else
            {
                return $"{name}, {quantity} {unit}\nCalories: {calories}\nFood Group: {foodGroup}";
            }


        }

        public static explicit operator Ingredient(ListBox v)
        {
            throw new NotImplementedException();
        }
    }
}
//-----------------------------------------------------------------------494949494949___END-OF-FILE___49494494949949------------------------------------------------\\