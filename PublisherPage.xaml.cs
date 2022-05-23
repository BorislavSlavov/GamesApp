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
    /// Логика взаимодействия для PublisherPage.xaml
    /// </summary>
    public partial class PublisherPage : Page
    {
        public PublisherPage()
        {
            InitializeComponent();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {


            Manager.MainFrame.Navigate(new AddEditPagePub((sender as Button).DataContext as Publishers));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PublishersForRemoving = DGridPublisher.SelectedItems.Cast<Publishers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PublishersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GamesEntities.GetContext().Publishers.RemoveRange(PublishersForRemoving);
                    GamesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridPublisher.ItemsSource = GamesEntities.GetContext().Publishers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPagePub(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GamesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridPublisher.ItemsSource = GamesEntities.GetContext().Publishers.ToList();
            }
        }
    }
}
