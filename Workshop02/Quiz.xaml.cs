using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Threading;

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        Question quizQuestion;
        DateTime start;
        public Quiz(Question question)
        {
            quizQuestion = question;
            InitializeComponent();

            label_question.Content = quizQuestion.question;
            btn_a.Content = quizQuestion.A; btn_a.Tag = "A";
            btn_b.Content = quizQuestion.B; btn_b.Tag = "B";
            btn_c.Content = quizQuestion.C; btn_c.Tag = "C";
            btn_d.Content = quizQuestion.D; btn_d.Tag = "D";
            start = DateTime.Now;
            Timer();
        }

        //Source: https://stackoverflow.com/questions/11719283/how-to-close-auto-hide-wpf-window-after-10-sec-using-timer
        private void Timer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += TimerResizer;
            timer.Start();
        }

        private async void TimerResizer(object sender, EventArgs e)
        {
            var size = 700 - 700 * ((DateTime.Now - start).TotalMilliseconds / 60000);
            if (size < 0)
            {
                message = false;
                Close();
            }

            time_top.Width = size;
            time_bottom.Width = size;
            Timer();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            message = false;
            if (sender is Button button && (string)button.Tag == quizQuestion.correct)
                DialogResult = true;

            Close();
        }

        bool message = true;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (message)
            {
                var box = MessageBox.Show("Biztos ki akar lépni?", "Kilépés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (box == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }
    }
}
