using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Question> questionList;

        public MainWindow()
        {
            questionList = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("questions.json")) ?? new List<Question>();
            InitializeComponent();
        }

        private void wrap_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < questionList.Count; i++)
            {
                var image = new Image
                {
                    Width = ActualWidth / 7,
                    Source = new BitmapImage(new Uri("image.jpg", UriKind.RelativeOrAbsolute))
                    
                };
                Button button = new Button()
                {
                    Background = Brushes.Transparent,
                    Content = image,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(4),
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Tag = i
                };
                button.Click += OpenQuestion;

                wrap.Children.Add(button);
            }
        }

        private void OpenQuestion(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (new Quiz(questionList[(int)button.Tag]).ShowDialog() == true)
                    button.BorderBrush = Brushes.Green;
                else
                    button.BorderBrush = Brushes.Red;

                button.Click -= OpenQuestion;
            }
        }
    }

    public class Question
    {
        public string question { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string correct { get; set; }
    }
}
