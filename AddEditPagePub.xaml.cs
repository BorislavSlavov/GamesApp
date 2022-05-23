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
    /// Логика взаимодействия для AddEditPagePub.xaml
    /// </summary>
    public partial class AddEditPagePub : Page
    {
        private Publishers _currentPublisher = new Publishers();
        public AddEditPagePub(Publishers selectedPublisher)
        {
            InitializeComponent();

            if (selectedPublisher != null)
                _currentPublisher = selectedPublisher;

            DataContext = _currentPublisher;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPublisher.Publisher_name))
                errors.AppendLine("Укажите название издателя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPublisher.Publisher_code == 0)
            {
                _currentPublisher.Publisher_code = GamesEntities.GetContext().Publishers.Select(x => x.Publisher_code).Max() + 1;
                GamesEntities.GetContext().Publishers.Add(_currentPublisher);
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
