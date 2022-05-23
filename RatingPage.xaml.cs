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
    /// Логика взаимодействия для RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        public RatingPage()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageRate((sender as Button).DataContext as Rating));
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageRate(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var RatingForRemoving = DGridRating.SelectedItems.Cast<Rating>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {RatingForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GamesEntities.GetContext().Rating.RemoveRange(RatingForRemoving);
                    GamesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    if (Authorization.code == 1)
                        DGridRating.ItemsSource = GamesEntities.GetContext().Rating.ToList();
                    else
                        DGridRating.ItemsSource = GamesEntities.GetContext().Rating.Where(p => p.User_code == Authorization.code).OrderByDescending(p => p.User_rating).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GamesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                if (Authorization.code == 1)
                    DGridRating.ItemsSource = GamesEntities.GetContext().Rating.ToList();
                else
                    DGridRating.ItemsSource = GamesEntities.GetContext().Rating.Where(p => p.User_code == Authorization.code).OrderByDescending(p => p.User_rating).ToList();
            }
        }
    }
}
