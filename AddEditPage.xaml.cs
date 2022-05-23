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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Games _currentGame = new Games();
        public AddEditPage(Games selectedGame)
        {
            InitializeComponent();

            if (selectedGame != null)
                _currentGame = selectedGame;

            DataContext = _currentGame;
            ComboGenre.ItemsSource = GamesEntities.GetContext().Genres.ToList();
            ComboDeveloper.ItemsSource = GamesEntities.GetContext().Developers.ToList();
            ComboPublisher.ItemsSource = GamesEntities.GetContext().Publishers.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentGame.Game_name))
                errors.AppendLine("Укажите название игры");
            if (_currentGame.Rate < 1 || _currentGame.Rate > 10)
                errors.AppendLine("Оценка игры - от 1 до 10");
            if (string.IsNullOrWhiteSpace(_currentGame.Description))
                errors.AppendLine("Укажите описание игры");
            if (_currentGame.Genres == null)
                errors.AppendLine("Выберите жанр");
            if (_currentGame.Developers == null)
                errors.AppendLine("Выберите разработчика");
            if (_currentGame.Publishers == null)
                errors.AppendLine("Выберите издателя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentGame.Game_code == 0)
            {  
                _currentGame.Game_code = GamesEntities.GetContext().Games.Select(x => x.Game_code).Max() + 1;    
                GamesEntities.GetContext().Games.Add(_currentGame);
            }
            try
            {
                GamesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }
    }
}
