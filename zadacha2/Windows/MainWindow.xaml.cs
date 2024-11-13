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
using System.Windows.Shapes;
using Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники.Pages;

namespace Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new AuthPage());
        }
        public void NavigateToManagerPage()
        {
            mainFrame.Navigate(new ManagerPage());
        }

        public void NavigateToOperPage()
        {
            mainFrame.Navigate(new OperatorPage());
        }
        public void NavigateToMasterPage()
        {
            mainFrame.Navigate(new MasterPage());
        }

        public void NavigateToClientPage()
        {
            mainFrame.Navigate(new ClientPage());
        }
    }
}
