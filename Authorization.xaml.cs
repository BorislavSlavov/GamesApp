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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static int code = 0;
        public static string _login; 
        string _password;
        public Authorization()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Users User = null;

            _login = LoginText.Text;
            _password = PasswordText.Password;
            User = GamesEntities.GetContext().Users.Where(p => p.Password == _password && p.Login == _login).FirstOrDefault();
                
            if (User == null) MessageBox.Show("Не найдено");
            else if (User.Login == "admin")
            {
                MessageBox.Show("Успешно");
                code = User.User_code;
                Manager.MainFrame.Content = new UserMenu();
            }
            else
            {
                MessageBox.Show("Успешно");
                code = User.User_code;
                Manager.MainFrame.Content = new ClientMenu();
            }

        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new Registration(null);
        }
    }
}
