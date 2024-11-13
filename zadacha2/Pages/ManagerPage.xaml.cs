using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using zadacha2;
using zadacha2.Windows;

namespace Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники.Pages
{
    public partial class ManagerPage : Page
    {
        private BDEntities bd = new BDEntities();
        private IQueryable<Order> allRequests;

        public ManagerPage()
        {
            InitializeComponent();
            LoadRequests();
        }

        // Загружаем заявки
        private void LoadRequests()
        {
            allRequests = bd.Orders.ToList().AsQueryable();
            requestsDataGrid.ItemsSource = allRequests.ToList();
        }

        private void RequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;

            if (selectedRequest != null && Session.currentUser != null)
            {
                // Проверяем, соответствует ли текущий пользователь назначенному на заявку работнику
                if (selectedRequest.SotrudnicId == Session.currentUser.ID)
                {
                    sendToProductionButton.IsEnabled = true;
                    replaceWorkerButton.IsEnabled = true;
                }
                else
                {
                    sendToProductionButton.IsEnabled = false;
                    replaceWorkerButton.IsEnabled = false;
                }
            }
            else
            {
                // Если заявка не выбрана или пользователь не авторизован, отключаем кнопки
                sendToProductionButton.IsEnabled = false;
                replaceWorkerButton.IsEnabled = false;
            }
        }

        private void SendToProductionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;
            if (selectedRequest != null && selectedRequest.StatusID == 1)
            {
                selectedRequest.StatusID = 2;
                try
                {
                    bd.SaveChanges();
                    MessageBox.Show("Статус заявки успешно изменен на 'В процессе ремонта'", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку со статусом 'Новая' для отправки в производство.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AssignWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;

            if (selectedRequest != null && Session.currentUser != null)
            {
                if (selectedRequest.SotrudnicId.HasValue)
                {
                    MessageBox.Show("Эта заявка уже принята другим сотрудником и не может быть изменена.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedRequest.SotrudnicId = Session.currentUser.ID;

                try
                {
                    bd.SaveChanges();
                    LoadRequests();
                    MessageBox.Show("Вы успешно добавили себя в качестве работника оформившего заявку!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку и убедитесь, что вы вошли в систему.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReplaceWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;
            if (selectedRequest != null && selectedRequest.StatusID == 2)
            {
                var existingTask = bd.Orders.FirstOrDefault(z => z.ID == selectedRequest.ID);

                if (existingTask != null)
                {
                    AssignWorkerWindow assignWorkerWindow = new AssignWorkerWindow(selectedRequest.ID, isReplacingWorker: true);
                    assignWorkerWindow.ShowDialog();
                    LoadRequests();
                }
                else
                {
                    MessageBox.Show("Для выбранной заявки не назначен мастер.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку со статусом 'В производстве' для замены мастера.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
