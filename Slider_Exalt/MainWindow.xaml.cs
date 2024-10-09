using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Slider_Exalt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                var uri = new Uri("pack://application:,,,/Images/cells2.png", UriKind.Absolute);
                var bitmap = new BitmapImage(uri);
                cellsImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de chargement de l'image");
            }
        }

        private void CustomSlider_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var slider = (Slider)sender;
                var canvas = (Canvas)slider.Template.FindName("GraduationsCanvas", slider);
                canvas.Children.Clear();

                var thumb = (Thumb)slider.Template.FindName("thumb", slider);
                double thumbWidth = thumb?.ActualWidth ?? 50;
                double thumbHeight = thumb?.ActualHeight ?? 20;
                double sliderHeight = canvas.ActualHeight;
                double sliderwidth = canvas.ActualWidth;

                // Ajouter des rectangles pour chaque graduation
                for (int i = 0; i <= 15; i++)
                {
                    var rectangle = new Rectangle
                    {
                        Width = sliderwidth - 10,
                        Height = 1,
                        Fill = Brushes.Black,
                    };
                    if (i == 0 || i == 15)
                    {
                        rectangle.Width += 20;
                    }
                    double position = (sliderHeight - thumbHeight) * (15 - i) / 15.0 + (thumbHeight / 2.0);
                    Canvas.SetTop(rectangle, position - (rectangle.Height / 2)); // Centrer verticalement

                    Canvas.SetLeft(rectangle, (canvas.ActualWidth - rectangle.Width) / 2); // Centrer horizontalement

                    canvas.Children.Add(rectangle); // Ajouter le rectangle au Canvas
                }

                // Gestion du changement de valeur
                slider.ValueChanged += (s, args) =>
                {
                    var thumbText = (TextBlock)thumb.Template.FindName("thumbText", thumb);
                    thumbText.Text = $"{slider.Value:F0} μm "; // Affiche la valeur en entier
                };

                // Mettre à jour le texte initial
                var initialText = (TextBlock)thumb.Template.FindName("thumbText", thumb);
                initialText.Text = $"{slider.Value:F0} μm";
            }
            catch (Exception ex)
            {
               Console.WriteLine("Erreur lors du chargement du slider ");
            }
        }

        private void UpArrow_Click(object sender, RoutedEventArgs e)
        {
            var slider = customSlider;
            if (slider.Value > slider.Minimum)
            {
                slider.Value -= 1; // Incrémente la valeur
            }
        }

        private void DownArrow_Click(object sender, RoutedEventArgs e)
        {
            var slider = customSlider;
            if (slider.Value < slider.Maximum)
            {
                slider.Value += 1; // Décrémente la valeur
            }
        }
    }
}