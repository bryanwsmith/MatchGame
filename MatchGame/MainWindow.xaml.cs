using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tengthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetupGame();
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐙","🐙",
                "🐡","🐡",
                "🐘","🐘",
                "🐳","🐳",
                "🐫","🐫",
                "🦕","🦕",
                "🦘","🦘",
                "🦔","🦔"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tengthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockedClicked;

        bool findMatching = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBloxk = sender as TextBlock;

            if (findMatching == false)
            {
                textBloxk.Visibility = Visibility.Hidden;
                lastTextBlockedClicked = textBloxk;
                findMatching = true;
            }
            else if (textBloxk.Text == lastTextBlockedClicked.Text)
            {
                matchesFound++;
                textBloxk.Visibility = Visibility.Hidden;
                findMatching = false;
            }
            else
            {
                lastTextBlockedClicked.Visibility = Visibility.Visible;
                findMatching = false;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tengthOfSecondsElapsed++;
            timeTextBlock.Text = (tengthOfSecondsElapsed / 10F).ToString("0.0s");
            
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetupGame();
            }

        }
    }
}
