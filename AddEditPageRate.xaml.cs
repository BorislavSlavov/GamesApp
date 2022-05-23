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
    /// Логика взаимодействия для AddEditPageRate.xaml
    /// </summary>
    public partial class AddEditPageRate : Page
    {
        private Rating _currentRate = new Rating();
        public AddEditPageRate(Rating selectedRate)
        {
            InitializeComponent();
            if (selectedRate != null)
                _currentRate = selectedRate;

            DataContext = _currentRate;
            ComboGame.ItemsSource = GamesEntities.GetContext().Games.ToList();
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentRate.User_rating < 1 || _currentRate.User_rating > 10)
                errors.AppendLine("Оценка игры - от 1 до 10");
            if (_currentRate.User_rating == null)
                errors.AppendLine("Введите оценку");
            if (_currentRate.Games == null)
                errors.AppendLine("Выберите игру");

            _currentRate.User_code = Authorization.code;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentRate.Rating_code == 0)
            {
                _currentRate.Rating_code = GamesEntities.GetContext().Rating.Select(x => x.Rating_code).Max() + 1;
                GamesEntities.GetContext().Rating.Add(_currentRate);
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
