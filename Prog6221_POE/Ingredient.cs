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

        public enum Unit
        {
            tsp,
            tbsp,
            cup,
            cups,
            other
        }

        public Unit unit { get; set; }

        public string otherUnit { get; set; }

        public Ingredient() { }

        public Ingredient(string nme, double qnty, double scle, Unit u, string oUnit)
        {
            name = nme;
            quantity = qnty;
            scale = scle;
            unit = u;
            otherUnit = oUnit;
        }

        public double Scale(double scale)
        {
            this.quantity *= scale;
            return scale;
        }

        public override string ToString()
        {
            string ing = "";
            if (unit == Unit.other)
            {
                ing = name + ", " + quantity + " " + otherUnit;
            }
            else
            {
                ing = name + ", " + quantity + " " + unit;
            }
            return ing;
        }

    }
}
