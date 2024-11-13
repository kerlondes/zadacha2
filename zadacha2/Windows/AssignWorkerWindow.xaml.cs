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

namespace zadacha2.Windows
{
    /// <summary>
    /// Логика взаимодействия для AssignWorkerWindow.xaml
    /// </summary>
    public partial class AssignWorkerWindow : Window
    {
        private BDEntities bd = new BDEntities();
        private int selectedRequestId;
        private bool isReplacingWorker;
        public AssignWorkerWindow(int requestId, bool isReplacingWorker = false)
        {
            InitializeComponent();
            selectedRequestId = requestId;
            this.isReplacingWorker = isReplacingWorker;
            LoadWorkers();
        }


        // Загрузка рабочих с ID_Dolj = 2
        private void LoadWorkers()
        {
            var workers = bd.Users.Where(r => r.RoleId == 2).ToList();
            workerComboBox.ItemsSource = workers;
            workerComboBox.DisplayMemberPath = "FIO";
            workerComboBox.SelectedValuePath = "ID";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (workerComboBox.SelectedItem != null)
            {
                int selectedWorkerId = (int)workerComboBox.SelectedValue;

                // Проверяем, существует ли уже задача с данным ZacazId
                var existingTask = bd.ZadachaZacasas.FirstOrDefault(z => z.ZacazId == selectedRequestId);

                if (existingTask != null)
                {
                    if (isReplacingWorker)
                    {
                        // Логика замены мастера
                        existingTask.SotrudnicId = selectedWorkerId;
                        existingTask.Date = DateTime.Now; // Обновляем дату начала, если нужно
                    }
                    else
                    {
                        MessageBox.Show("Задача для этого заказа уже существует. Используйте опцию замены мастера, если необходимо.",
                                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    // Создание новой задачи, если она еще не создана
                    var newTask = new ZadachaZacasa
                    {
                        SotrudnicId = selectedWorkerId,
                        ZacazId = selectedRequestId,
                        Date = DateTime.Now
                    };
                    bd.ZadachaZacasas.Add(newTask);
                }

                try
                {
                    bd.SaveChanges();
                    MessageBox.Show(isReplacingWorker ? "Мастер успешно заменен." : "Мастер успешно назначен на задачу.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите рабочего для назначения или замены.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
