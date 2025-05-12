using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VDK
{
    /// <summary>
    /// Логика взаимодействия для Image_zoom.xaml
    /// </summary>
    public partial class Image_zoom : Window
    {
        public Image_zoom(BitmapImage imageb)
        {
            InitializeComponent();
            image.Source = imageb;
        }

        // Обработчик закрытия окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true; // Размораживаю MainWindow
        }
    }
}
