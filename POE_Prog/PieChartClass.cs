using System;
using System.Windows.Media;

namespace POE_Prog
{
    public class PieChartClass
    {
        public double Value { get; set; }
        public double Percentage { get; set; } // Represents the percentage of the total this value contributes to
        public Color Color { get; set; }       // Color of the pie slice representing this data
        public string Label { get; set; }      // Label or description of the data

        public PieChartClass() { }

        public PieChartClass(double value, Color color, string label, double percentage)
        {
            Value = value;
            Color = color;
            Label = label;
            Percentage = percentage;
        }
    }
}