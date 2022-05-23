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

namespace GamesApp
{
    /// <summary>
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Page
    {
        public UserMenu()
        {
            InitializeComponent();
            UserInfo.Text = Authorization._login;
        }

        private void Games_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new GamesListPage();
        }

        private void GamesTable_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new GamesPage();
        }

        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new GenrePage();
        }

        private void Dev_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new DeveloperPage();
        }

        private void Pub_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new PublisherPage();
        }

        private void Rating_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new RatingPage();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new UsersPage();
        }
    }
}
