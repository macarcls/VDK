using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.IO;
using Microsoft.Windows.Input;

namespace VDK
{
    internal class View_Model_INPC : INotifyPropertyChanged
    {
        private Model_INPC? selected;

        public ObservableCollection<Model_INPC>? Model { get; set; }
        public Model_INPC Selected 
        { 
            get { return selected! ?? new Model_INPC { SeriesCollection = Series(), Labels = Label(), Formatter = Form() }; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public View_Model_INPC()
        {
            Model = new ObservableCollection<Model_INPC>
            {
                new Model_INPC { content = "Model 1", SeriesCollection = Series(), Labels = Label(), Formatter = Form() },
                new Model_INPC { content = "Model 2" },
                new Model_INPC { content = "Model 3" },
                new Model_INPC { content = "Model 4" }
            };
        }

        protected SeriesCollection Series()
        {
            SeriesCollection SeriesCollection;
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
            return SeriesCollection;
        }

        protected string[] Label()
        {
            string[] Labels = { "0,48-19,44", "19,44-38,40", "38,40-57,36", "57,36-76,32", "76,32-95,28", "95,28-114,24", "114,24-133,21" }; 
            return Labels;
        }

        protected Func<double, string> Form() 
        {
            
            Func<double, string> Formatter = value => value + "%";
            return Formatter;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
