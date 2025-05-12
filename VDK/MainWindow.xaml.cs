using LiveCharts.Wpf;
using LiveCharts;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Runtime.Serialization;

namespace VDK;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Resourses _viewModel;
    public MainWindow()
    {
        InitializeComponent();
     
        _viewModel = new Resourses();
        DataContext = _viewModel;
        
    }

    private void Upload_photo_Click(object sender, RoutedEventArgs e)
    {
        // Open dialog
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.DefaultExt = ".png";
        bool? result = dialog.ShowDialog();


        // Process open file dialog box results
        if (result == true)
        {
            // Записываю путь к файлу в filename и создаю битмап
            _viewModel.filename = dialog.FileName;
            BitmapImage imageBitm = new BitmapImage(new Uri(_viewModel.filename, UriKind.Absolute));

            // Обрезаю круговое изображение
            var circ_crop = new CroppedBitmap(imageBitm, new Int32Rect(512, 512, 200, 200));
            var imageBrush = new ImageBrush
            {
                ImageSource = circ_crop,
                Stretch = Stretch.None
            };

            
            cropped.Source = new CroppedBitmap(imageBitm, new Int32Rect(0, 1024, 512, 120)); // Информация по фотографии
            image_p.Source = new CroppedBitmap(imageBitm, new Int32Rect(0, 0, 1024, 1024)); // Полное изображение
            ellipse.Fill = imageBrush;
            Zoom_im.IsEnabled = true;
            Zoom_im.Visibility = Visibility.Visible; 
        }
    }
    
    // Обработчик нажатия по изображению (открывает окно Image_zoom)
    private void ZoomPhoto(object sender, RoutedEventArgs e)
    {

        BitmapImage imageBitm = new BitmapImage(new Uri(_viewModel.filename!, UriKind.Absolute));
        Image_zoom zoom = new Image_zoom(imageBitm);
        zoom.Owner = this; // Делаю связь для обращения из Image_zoom к MainWindow
        zoom.Show();
        mainw.IsEnabled = false; // Замораживаю окно
    }
}