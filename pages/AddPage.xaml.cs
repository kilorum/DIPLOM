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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {

        private static Insurance _current = new Insurance();
        public AddPage(Insurance selectedItem)
        {
            InitializeComponent();

            if (selectedItem != null)
                _current = selectedItem;

            DataContext = _current;

            BrandBox.ItemsSource = DIPLOMEntities1.GetContext().Brand.ToList();
            StatusBox.ItemsSource = DIPLOMEntities1.GetContext().Status.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrEmpty(_current.Name_Client))
                errors.AppendLine("Введите Имя клиента");

            if (_current.NumberPassport < 10000000 || _current.NumberPassport > 99999999)
                errors.AppendLine("Введите 8ми значный номер паспорта");

            if (_current.Brand == null)
                errors.AppendLine("Выберите бранд машины ");

            if (string.IsNullOrEmpty(_current.model))
                errors.AppendLine("Введите модель машины");

            if (string.IsNullOrEmpty(_current.year) || _current.year.Length != 4)
                errors.AppendLine("Введите год выпуска машины");

            if (_current.Status == null)
                errors.AppendLine("Выберите статус страховки");

            if (_current.Sum < 0)
                errors.AppendLine("Введите сумму страховки");

            if (string.IsNullOrEmpty(_current.Techical_Condition))
                errors.AppendLine("Введите техническое описание");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current.ID == 0)
                DIPLOMEntities1.GetContext().Insurance.Add(_current);

            try
            {
                DIPLOMEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация добавлена");
                manager.MainFrame.Navigate(new General());
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new General());
        }
    }
}
