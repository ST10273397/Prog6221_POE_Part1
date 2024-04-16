using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prog6221_POE
{
    internal class Ingredient
    {
        public string name {  get; set; }

        public double quantity { get; set; }

        public double scale { get; set; }

        public string unit { get; set; }

        public Ingredient() { }

        public Ingredient(string nme, double qnty, double scle, string u)
        {
            name = nme;
            quantity = qnty;
            scale = scle;
            unit = u;
        }

        public double Scale(double scale)
        {
            this.quantity *= scale;
            return scale;
        }

        public override string ToString()
        {
            string ing = name + ", " + quantity + " " + unit;
            return ing;
        }

    }
}
