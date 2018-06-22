using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitterClient;

namespace Twitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Tweet> tweets;

        public MainWindow()
        {
            InitializeComponent();
            this.tweets = new List<Tweet>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if(!String.IsNullOrEmpty(textBox.Text))
            {
                var text = textBox.Text;
                var task = new Task<List<Tweet>>((t) =>
                {
                    return Client.GetTweets((string)t, 5);
                }, text);

                task.Start();
                this.Tweets.ItemsSource = null;
                this.tweets = task.Result;
                this.Tweets.ItemsSource = this.tweets;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.HashTag.Text))
            {
                this.tweets.AddRange(Client.GetTweets(this.HashTag.Text, 3));
                this.Tweets.ItemsSource = null;
                this.Tweets.ItemsSource = this.tweets;
            }
        }
    }
}
