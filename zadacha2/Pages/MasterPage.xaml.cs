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
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        private BDEntities bd = new BDEntities();
        public MasterPage()
        {
            InitializeComponent();
            LoadRequests();
            LoadParts();
        }
        // Загрузка заявок для текущего мастера
        private void LoadRequests()
        {
            int currentMasterId = Session.currentUser.ID;
            var tasks = bd.ZadachaZacasas
                .Where(z => z.SotrudnicId == Session.currentUser.ID)
                .ToList();

            var requests = tasks
                .Select(t => t.Order)
                .Where(z => z.StatusID == 2 || z.StatusID == 4)
                .ToList();

            requestsDataGrid.ItemsSource = requests;
        }
        // Загрузка списка всех запчастей
        private void LoadParts()
        {
            var parts = bd.Zapchasts.ToList();
            partsListBox.ItemsSource = parts;
        }
        private void AddPartButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;
            var selectedPart = partsListBox.SelectedItem as Zapchast;

            if (selectedRequest != null && selectedPart != null)
            {
                var task = bd.ZadachaZacasas
                    .FirstOrDefault(z => z.ZacazId == selectedRequest.ID && z.SotrudnicId == Session.currentUser.ID);
                if (task != null)
                {
                    var taskPart = new Zadacha_zapchast
                    {
                        ZadachaId = task.ID,
                        ZapchastId = selectedPart.ID
                    };
                    bd.Zadacha_zapchast.Add(taskPart);
                    selectedRequest.StatusID = 3;
                    try
                    {
                        bd.SaveChanges();
                        MessageBox.Show("Запчасть добавлена к задаче, статус заявки обновлен.");
                        LoadRequests(); // Перезагружаем заявки
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении запчасти: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Не найдена задача для выбранной заявки.");
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку и запчасть для добавления.");
            }
        }
        // Обработчик кнопки "Отремонтировано"
        private void CompleteRepairButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = requestsDataGrid.SelectedItem as Order;

            if (selectedRequest != null)
            {
                selectedRequest.StatusID = 3;
                selectedRequest.DateFin = DateTime.Now;

                var task = bd.ZadachaZacasas.FirstOrDefault(z => z.ZacazId == selectedRequest.ID);
                if (task != null)
                {

                    task.DateFin = DateTime.Now;
                    task.Description = commentsTextBox.Text; // Запись комментария мастера
                }
                try
                {
                    bd.SaveChanges();
                    MessageBox.Show("Заявка отмечена как отремонтированная.");
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении ремонта: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для завершения ремонта.");
            }
        }
    }
}
