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
    /// Логика взаимодействия для ClientMenu.xaml
    /// </summary>
    public partial class ClientMenu : Page
    {
        public ClientMenu()
        {
            InitializeComponent();
            UserInfo.Text = Authorization._login;
        }

        private void Rating_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new RatingPage();
        }

        private void ShowGames_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new GamesListPage();
        }

    }
}
