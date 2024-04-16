using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog6221_POE
{
    internal class Step
    {
        public int StepNumber { get; set; }

        public string StepDescription { get; set; }

        public Step() { }

        public Step(int stepNum, string stepDesc)
        {
            StepDescription = stepDesc;
        }

        public override string ToString()
        {
            string ing = StepDescription;
            return ing;
        }
    }
}
