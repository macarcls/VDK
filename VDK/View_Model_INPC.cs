using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VDK
{
    internal class View_Model_INPC : INotifyPropertyChanged
    {
        private Model_INPC? selected;

        public ObservableCollection<Model_INPC>? Model { get; set; }
        public Model_INPC Selected 
        { 
            get { return selected!; }
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
                new Model_INPC { content = "Model 1" },
                new Model_INPC { content = "Model 2" },
                new Model_INPC { content = "Model 3" },
                new Model_INPC { content = "Model 4" }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
