using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VDK
{
    internal class Resourses
    {
        public string? filename { get; set; }
        public SeriesCollection? SeriesCollection { get; set; }
        public string[]? Labels { get; set; }
        public Func<double, string>? Formatter { get; set; }
 
        
        public Resourses()
        {
            /* График */
            SeriesCollection = new SeriesCollection
            {
                        new ColumnSeries
                            {
                                Title = "percentaj",
                                Values = new ChartValues<double> { 10, 0, 70, 10, 5, 5, 0 },
                                DataLabels = true,
                                LabelPoint = point => point.Y + "%"
                            },
            };

            Labels = new[] { "0,48-19,44", "19,44-38,40", "38,40-57,36", "57,36-76,32", "76,32-95,28", "95,28-114,24", "114,24-133,21" };
            Formatter = value => value + "%";
        }
    }
}
