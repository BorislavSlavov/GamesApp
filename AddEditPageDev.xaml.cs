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
    /// Логика взаимодействия для AddEditPageDev.xaml
    /// </summary>
    public partial class AddEditPageDev : Page
    {
        private Developers _currentDevelopers = new Developers();
        public AddEditPageDev(Developers selectedDevelopers)
        {
            InitializeComponent();

            if (selectedDevelopers != null)
                _currentDevelopers = selectedDevelopers;

            DataContext = _currentDevelopers;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentDevelopers.Developer_name))
                errors.AppendLine("Укажите название разработчика");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentDevelopers.Developer_code == 0)
            {
                _currentDevelopers.Developer_code = GamesEntities.GetContext().Developers.Select(x => x.Developer_code).Max() + 1;
                GamesEntities.GetContext().Developers.Add(_currentDevelopers);
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
