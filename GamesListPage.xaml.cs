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
    /// Логика взаимодействия для GamesListPage.xaml
    /// </summary>
    public partial class GamesListPage : Page
    {
        public GamesListPage()
        {
            InitializeComponent();

            var allGenres = GamesEntities.GetContext().Genres.ToList();
            allGenres.Insert(0,new Genres
            {
                Genre_name = "Все жанры"
            });
            ComboGenre.ItemsSource = allGenres;
            CheckStatus.IsChecked = true;

            UpdateGames();
        }

        private void UpdateGames()
        {
            var currentGames = GamesEntities.GetContext().Games.ToList();
         
            var Search = TBoxSearch.Text.ToLower();
            if (ComboGenre.SelectedIndex != 0) {
            var SelectedGenre = ComboGenre.SelectedItem as Genres;
            currentGames = currentGames.Where(p => p.Game_name.ToLower().Contains(Search) && SelectedGenre.Genre_code == p.Genre_code).ToList();
            }
            else
            currentGames = currentGames.Where(p => p.Game_name.ToLower().Contains(Search)).ToList();

            if (CheckStatus.IsChecked.Value)
                currentGames = currentGames.Where(p => p.Game_status).ToList();

            LViewGames.ItemsSource = currentGames.OrderBy(p => p.Game_code).ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGames();
        }

        private void ComboGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGames();
        }

        private void CheckStatus_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGames();
        }
    }
}
