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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private int reg = 0;
        private Users _currentUser = new Users();
        int maxid = GamesEntities.GetContext().Users.Max(u => u.User_code);
        
        public Registration(Users SelectUser)
        {
            InitializeComponent();
            if (SelectUser != null)
            {
                _currentUser = SelectUser;
                reg = 1;
                PasswordText.Password = _currentUser.Password;
            }
            else
            {
                _currentUser.User_code = maxid + 1;
            }
            DataContext = _currentUser;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var TestLogin = GamesEntities.GetContext().Users.Where(p => p.Login == LoginText.Text).FirstOrDefault();
            _currentUser.Password = PasswordText.Password;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login)) errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(_currentUser.Password)) errors.AppendLine("Введите пароль");
            if (TestLogin != null) errors.AppendLine("Такое имя пользователя уже сущетсвует!");
          
            string str; int i = 0; bool bol; int x = 0;
            if (_currentUser.Password.Length < 6) errors.AppendLine("Пароль должен быть длиннее 6 символов");
            str = _currentUser.Password.ToLower();
            if (_currentUser.Password == str) errors.AppendLine("В пароле должна быть хотя бы 1 большая буква");
            char[] array = _currentUser.Password.ToCharArray();
            while (_currentUser.Password.Length > i)
            {
                bol = Char.IsDigit(array[i]);
                if (bol == true) x = x + 1;
                i = i + 1;
            }
            if (_currentUser.Password.Length <= x) errors.AppendLine("Пароль должен включать в себя большие и маленькие буквы");
            if (x == 0) errors.AppendLine("Пароль должен включать в себя цифры");
          
            if (_currentUser.Login != null)
            {
                if (_currentUser.Login.Length > 50) errors.AppendLine("Логин должен быть короче 50 символов");
            }
            if (_currentUser.Password != null)
            {
                if (_currentUser.Password.Length > 50) errors.AppendLine("Пароль должен быть короче 50 символов");
            }
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }

            if (reg == 0) GamesEntities.GetContext().Users.Add(_currentUser);
            else
            {
                var usrs = GamesEntities.GetContext().Users.Where(p => p.User_code == _currentUser.User_code).FirstOrDefault();
                usrs.Login = _currentUser.Login;
                usrs.Password = _currentUser.Password;
            }
            try
            {
                GamesEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Вы зарегистрированы!");
                else MessageBox.Show("Данные изменены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
