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
    /// Логика взаимодействия для GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage()
        {
            InitializeComponent();
            //DGridGames.ItemsSource = GamesEntities.GetContext().Games.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {


            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Games));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {   
            var gamesForRemoving = DGridGames.SelectedItems.Cast<Games>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {gamesForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GamesEntities.GetContext().Games.RemoveRange(gamesForRemoving);
                    GamesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridGames.ItemsSource = GamesEntities.GetContext().Games.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GamesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridGames.ItemsSource = GamesEntities.GetContext().Games.ToList();
            }
        }
    }
}
