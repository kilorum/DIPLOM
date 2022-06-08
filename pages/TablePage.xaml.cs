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

namespace DIPLOM.pages
{
    /// <summary>
    /// Логика взаимодействия для TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        {
            InitializeComponent();

            //GAuto.ItemsSource = DIPLOMEntities1.GetContext().Insurance.ToList();

            var allBrand = DIPLOMEntities1.GetContext().Brand.ToList();

            allBrand.Insert(0, new Brand
            {
                Name = "Все бранды"
            });
            Search_Model.ItemsSource = allBrand;
            Search_Model.SelectedIndex = 0;
            var allStatus = DIPLOMEntities1.GetContext().Status.ToList();
            allStatus.Insert(0, new Status
            {
                Status_name = "Все оценки"
            });
            Search_Status.ItemsSource = allStatus;
            Search_Status.SelectedIndex = 0;
            UpdateAuto();

            if(EnterPage.global.UserRoly == 2)
            {
                BtnAdd.Visibility = Visibility.Hidden;
                BtnDel.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new AddPage(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var DelAuto = GAuto.SelectedItems.Cast<Insurance>().ToList();
            if(MessageBox.Show($"Вы уверенны, что хотите удалить {DelAuto.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DIPLOMEntities1.GetContext().Insurance.RemoveRange(DelAuto);
                    DIPLOMEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно");
                    manager.MainFrame.Navigate(new General());
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAuto();
        }

        private void Search_Model_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateAuto();
        }

        private void Search_Status_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateAuto();
        }

        private void UpdateAuto()
        {
            var currentAuto = DIPLOMEntities1.GetContext().Insurance.ToList();

            if (Search_Model.SelectedIndex > 0)
            {
                string models = Search_Model.Text;
                currentAuto = currentAuto.Where(p => p.Brand.id == Search_Model.SelectedIndex).ToList();
            }

            if (Search_Model.SelectedIndex > 0)
            {
                string statu = Search_Model.Text;
                currentAuto = currentAuto.Where(p => p.Status.ID == Search_Status.SelectedIndex).ToList(); 
            }

            currentAuto = currentAuto.Where(p => p.Name_Client.Contains(BoxName.Text)).ToList();

            // привязка данных для отображения в DataGrid данных из таблицы БД
            GAuto.ItemsSource = currentAuto.OrderBy(p => p.Name_Client).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new AddPage((sender as Button).DataContext as Insurance));
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            UpdateAuto();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
