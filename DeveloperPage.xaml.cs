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
    /// Логика взаимодействия для DeveloperPage.xaml
    /// </summary>
    public partial class DeveloperPage : Page
    {
        public DeveloperPage()
        {
            InitializeComponent();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {


            Manager.MainFrame.Navigate(new AddEditPageDev((sender as Button).DataContext as Developers));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var DevelopersForRemoving = DGridDevelopers.SelectedItems.Cast<Developers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {DevelopersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GamesEntities.GetContext().Developers.RemoveRange(DevelopersForRemoving);
                    GamesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridDevelopers.ItemsSource = GamesEntities.GetContext().Developers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageDev(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GamesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridDevelopers.ItemsSource = GamesEntities.GetContext().Developers.ToList();
            }
        }
    }
}
