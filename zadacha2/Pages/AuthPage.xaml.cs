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
using zadacha2;

namespace Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private BDEntities m_entities = BDEntities.GetInstance();
        public AuthPage()
        {
            InitializeComponent();
            m_mainWindow = Application.Current.MainWindow as MainWindow;
        }
        private readonly MainWindow m_mainWindow;
        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (Password.Visibility == Visibility.Visible)
            {
                // Если видим PasswordBox, показываем TextBox с текстом
                PasswordTextBox.Text = Password.Password;
                Password.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                EyeIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("/icon/glas.jpg", System.UriKind.Relative)); // Открытый глаз
            }
            else
            {
                // Если видим TextBox, переключаем обратно на PasswordBox с символами
                Password.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Visible;
                EyeIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("/icon/neglas.jpg", System.UriKind.Relative)); // Закрытый глаз
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }

            var user = m_entities.Users
            .Where(a => a.Login == Login.Text)
            .FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            if (user.Password != Password.Password)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            Session.currentUser = user;

            switch (user.Role.Name)
            {
                case "Менеджер":
                    m_mainWindow.NavigateToManagerPage();
                    break;
                case "Мастер":
                    m_mainWindow.NavigateToMasterPage();
                    break;
                case "Оператор":
                    m_mainWindow.NavigateToOperPage();
                    break;
                case "Клиент":
                    m_mainWindow.NavigateToClientPage();
                    break;
            }
        }
    }
}
