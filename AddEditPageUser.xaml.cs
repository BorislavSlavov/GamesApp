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
    /// Логика взаимодействия для AddEditPageUser.xaml
    /// </summary>
    public partial class AddEditPageUser : Page
    {
        private Users _currentUsers = new Users();
        public AddEditPageUser(Users selectedUsers)
        {
            InitializeComponent();

            if (selectedUsers != null)
                _currentUsers = selectedUsers;

            DataContext = _currentUsers;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUsers.Login))
                errors.AppendLine("Укажите имя пользователя");
            if (string.IsNullOrWhiteSpace(_currentUsers.Password))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUsers.User_code == 0)
            {
                _currentUsers.User_code = GamesEntities.GetContext().Users.Select(x => x.User_code).Max() + 1;
                GamesEntities.GetContext().Users.Add(_currentUsers);
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
