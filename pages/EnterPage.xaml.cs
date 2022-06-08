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

namespace DIPLOM
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        public EnterPage()
        {
            InitializeComponent();
        }
        public static class global
        {
            public static int UserRoly;
        }
        private void EntBut_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DIPLOMEntities1())
            {
                var user_e = db.User
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == LoginBox.Text && u.Password == PasswordBox.Password);

                if (user_e == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }

                if (user_e.Roles.Roly == "Administrator")
                {
                    global.UserRoly = 1;
                    
                }

                if (user_e.Roles.Roly == "Manager")
                {
                    global.UserRoly = 2;
                }

                manager.MainFrame.Navigate(new General());
            }
        }

        private void EntBut1_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new General());
        }
    }
}
