using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VDK
{
    internal class Model_INPC : INotifyPropertyChanged
    {
        public string? content;
        public SeriesCollection? SeriesCollection;
        public string[]? Labels;
        public Func<double, string>? Formatter;

        public string Title
        {
            get { return content!; }
            set
            {
                content = value;
                OnPropertyChanged("Title");
            }
        }

        public SeriesCollection SC
        {
            get { return SeriesCollection!; }
            set
            {
                SeriesCollection = value;
                OnPropertyChanged("SC");
            }
        }
        public string[] Lab
        {
            get { return Labels!; }
            set
            {
                Labels = value;
                OnPropertyChanged("Lab");
            }
        }
        public Func<double, string>? Form
        {
            get { return Formatter; }
            set
            {
                Formatter = value;
                OnPropertyChanged("Form");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
