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
    /// Логика взаимодействия для GenrePage.xaml
    /// </summary>
    public partial class GenrePage : Page
    {
        public GenrePage()
        {
            InitializeComponent();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {


            Manager.MainFrame.Navigate(new AddEditPageGenre((sender as Button).DataContext as Genres));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var GenresForRemoving = DGridGenres.SelectedItems.Cast<Genres>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {GenresForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GamesEntities.GetContext().Genres.RemoveRange(GenresForRemoving);
                    GamesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridGenres.ItemsSource = GamesEntities.GetContext().Genres.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageGenre(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GamesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridGenres.ItemsSource = GamesEntities.GetContext().Genres.ToList();
            } 
        }
    }
}