using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog6221_POE
{
    internal class Step
    {
        //Properties to store step number and description
        public int StepNumber { get; set; }

        public string StepDescription { get; set; }

        //Default constructor
        public Step() { }

//---------------------------------------------------------------------------------\\

        /// <summary>
        /// Constructor with parameters to initialize step number and description
        /// </summary>
        /// <param name="stepNum"></param>
        /// <param name="stepDesc"></param>
        public Step(int stepNum, string stepDesc)
        {
            //Setting the step description property
            StepDescription = stepDesc;
        }

//---------------------------------------------------------------------------------\\


        /// <summary>
        /// Override ToString() method to return the step description
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Returning the step description
            string ing = StepDescription;
            return ing;
        }
    }
}
//------------------------------------------------------4949494944949___END-OF-FILE___49494949494949--------------------------------------------\\
