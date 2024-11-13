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
using zadacha2.Windows;

namespace Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public BDEntities bd = BDEntities.GetInstance();
        private IQueryable<Order> clientRequests;

        public ClientPage()
        {
            InitializeComponent();
            LoadClientRequests();
        }

        // Загрузка заявок клиента
        private void LoadClientRequests()
        {
            // Проверка, что текущий пользователь установлен
            if (Session.currentUser == null)
            {
                MessageBox.Show("Клиент не найден.");
                return;
            }

            // Заполняем ComboBox проблемами из базы данных
            var problems = bd.Problems.ToList();
            filterComboBox.ItemsSource = problems;
            filterComboBox.DisplayMemberPath = "Name"; // Имя проблемы для отображения
            filterComboBox.SelectedValuePath = "ID";   // ID проблемы для фильтрации

            // Загружаем заявки текущего пользователя
            clientRequests = bd.Orders.Where(order => order.User.ID == Session.currentUser.ID);
            ApplyFilters(); // Применяем фильтры к заявкам при начальной загрузке
        }

        // Применяет фильтры ComboBox и TextBox к DataGrid
        private void ApplyFilters()
        {
            // Получаем все заявки текущего пользователя
            var filteredRequests = clientRequests;

            // Фильтрация по проблеме, если выбрана проблема
            if (filterComboBox.SelectedItem is Problem selectedProblem)
            {
                filteredRequests = filteredRequests.Where(order => order.ProblemId == selectedProblem.ID);
            }

            // Фильтрация по номеру заказа, если в TextBox введено число
            if (int.TryParse(searchTextBox.Text, out int orderId))
            {
                filteredRequests = filteredRequests.Where(order => order.ID == orderId);
            }

            // Обновляем DataGrid с отфильтрованными заявками
            requestsDataGrid.ItemsSource = filteredRequests.ToList();
        }

        // Событие при изменении выбранной проблемы в ComboBox
        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        // Событие при изменении текста в TextBox
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        // Обработчик добавления заявки
        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var addRequestWindow = new AddOrderWindow();
            addRequestWindow.ShowDialog();
            LoadClientRequests();
        }

        // Обработчик редактирования заявки
        private void EditRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;

            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку для редактирования.");
                return;
            }
            if (selectedRequest.StatusID == 3)
            {
                MessageBox.Show("Эту заявку нельзя редактировать, так как её статус не позволяет изменений.");
                return;
            }

            var editRequestWindow = new EditOrderWindow(selectedRequest);
            editRequestWindow.ShowDialog();
            LoadClientRequests();
        }

        private void searchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _); // Ограничение ввода только чисел
        }
    }
}
