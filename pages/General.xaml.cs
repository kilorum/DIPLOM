﻿using System;
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
    /// Логика взаимодействия для General.xaml
    /// </summary>
    public partial class General : Page
    {
        public General()
        {
            InitializeComponent();
            manager2.GeneralFrame = GeneralFrame;
            manager2.GeneralFrame.Navigate(new pages.TablePage());

           
        }

    }
}
