using Prog6221_POE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace POE_Prog
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Page
    {
        public PieChart(Dictionary<string, double> foodGroupData)
        {
            InitializeComponent();

            // Check if foodGroupData is null or empty
            if (foodGroupData == null || foodGroupData.Count == 0)
                throw new ArgumentException("Food group data must be non-null and contain at least one entry.", nameof(foodGroupData));

            // Prepare data for the pie chart
            List<PieChartClass> pieData = PreparePieChartData(foodGroupData);

            // Draw the pie chart on the canvas
            DrawPieChart(pieData);
        }

//-------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A List that will prepare the pie chart data
        /// </summary>
        /// <param name="foodGroupData"></param>
        /// <returns></returns>
        private List<PieChartClass> PreparePieChartData(Dictionary<string, double> foodGroupData)
        {
            List<PieChartClass> pieData = new List<PieChartClass>();
            Random random = new Random();

            // Calculate the total sum of values for percentage calculation
            double total = 0;
            foreach (var data in foodGroupData)
            {
                total += data.Value;
            }

            // Generate pie chart data with random colors
            foreach (var data in foodGroupData)
            {
                // Calculate percentage for each slice
                double percentage = (data.Value / total) * 100;

                // Add data to pieData list
                pieData.Add(new PieChartClass
                {
                    Value = data.Value,
                    Color = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    Label = data.Key,
                    Percentage = percentage
                });
            }

            return pieData;
        }

//-------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A method that will draw the pie chart based on the Pie charts data
        /// </summary>
        /// <param name="pieData"></param>
        /// <exception cref="ArgumentException"></exception>
        private void DrawPieChart(List<PieChartClass> pieData)
        {
            // Check if pieData is null or empty
            if (pieData == null || pieData.Count == 0)
                throw new ArgumentException("Pie data must be non-null and have at least one element.", nameof(pieData));

            // Calculate pie chart dimensions
            double radius = Math.Min(pieChartCanvas.Width, pieChartCanvas.Height) / 2;
            double centerX = pieChartCanvas.Width / 2;
            double centerY = pieChartCanvas.Height / 2;
            double startAngle = 0;

            // Draw each pie slice
            foreach (var data in pieData)
            {
                double sliceAngle = (data.Value / pieData.Sum(p => p.Value)) * 360;

                // Draw pie slice
                DrawPieSlice(pieChartCanvas, centerX, centerY, radius, startAngle, startAngle + sliceAngle, data.Color);

                // Draw label
                DrawLabel(pieChartCanvas, centerX, centerY, radius, startAngle, sliceAngle, data.Label, data.Percentage);

                // Update startAngle for the next slice
                startAngle += sliceAngle;
            }
        }

//------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A method that uses math to draw the pie chart
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <param name="color"></param>
        private void DrawPieSlice(Canvas canvas, double centerX, double centerY, double radius, double startAngle, double endAngle, Color color)
        {
            Point startPoint = new Point(
                centerX + radius * Math.Cos(startAngle * Math.PI / 180),
                centerY + radius * Math.Sin(startAngle * Math.PI / 180)
            );
            Point endPoint = new Point(
                centerX + radius * Math.Cos(endAngle * Math.PI / 180),
                centerY + radius * Math.Sin(endAngle * Math.PI / 180)
            );

            bool isLargeArc = (endAngle - startAngle) > 180.0;

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(centerX, centerY);
            pathFigure.Segments.Add(new LineSegment(startPoint, true));
            pathFigure.Segments.Add(new ArcSegment(endPoint, new Size(radius, radius), 0, isLargeArc, SweepDirection.Clockwise, true));
            pathFigure.Segments.Add(new LineSegment(new Point(centerX, centerY), true));

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Fill = new SolidColorBrush(color),
                Data = pathGeometry
            };

            canvas.Children.Add(path);
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Method that will uses math to determine the labels and add them
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="startAngle"></param>
        /// <param name="sliceAngle"></param>
        /// <param name="label"></param>
        /// <param name="percentage"></param>
        private void DrawLabel(Canvas canvas, double centerX, double centerY, double radius, double startAngle, double sliceAngle, string label, double percentage)
        {
            double midAngle = startAngle + sliceAngle / 2;
            double labelRadius = radius + 35; // Distance from the center for the label

            Point labelPoint = new Point(
                centerX + labelRadius * Math.Cos(midAngle * Math.PI / 180),
                centerY + labelRadius * Math.Sin(midAngle * Math.PI / 180)
            );

            TextBlock textBlock = new TextBlock
            {
                Text = $"{label}: {percentage:F2}%",
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold
            };

            canvas.Children.Add(textBlock);

            // Position the label in the canvas
            textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = textBlock.DesiredSize;
            Canvas.SetLeft(textBlock, labelPoint.X - size.Width / 2);
            Canvas.SetTop(textBlock, labelPoint.Y - size.Height / 2);
        }

//---------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// A Button that will send the user back to the View Recipe Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            NavigationService.GoBack();
        }
    }
}
//----------------------------------------------------------___49494994949944949___END-OF-FILE___499494994949494949___-----------------------------------------------------//