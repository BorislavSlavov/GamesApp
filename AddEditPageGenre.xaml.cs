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
    /// Логика взаимодействия для AddEditPageGenre.xaml
    /// </summary>
    public partial class AddEditPageGenre : Page
    {
        private Genres _currentGenres = new Genres();
        public AddEditPageGenre(Genres selectedGenres)
        {
            InitializeComponent();

            if (selectedGenres != null)
                _currentGenres = selectedGenres;

            DataContext = _currentGenres;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentGenres.Genre_name))
                errors.AppendLine("Укажите название издателя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentGenres.Genre_code == 0)
            {
                _currentGenres.Genre_code = GamesEntities.GetContext().Genres.Select(x => x.Genre_code).Max() + 1;
                GamesEntities.GetContext().Genres.Add(_currentGenres);
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
