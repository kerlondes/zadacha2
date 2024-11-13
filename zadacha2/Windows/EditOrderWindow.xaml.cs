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
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        private Order _selectedRequest;
        private BDEntities gl = new BDEntities();
        public EditOrderWindow(Order selectedRequest)
        {
            InitializeComponent();
            _selectedRequest = selectedRequest;
            LoadRequestData();
        }
        private void LoadRequestData()
        {
            modelTextBox.Text = _selectedRequest.Model;

            // Загружаем проблемы в ComboBox
            var problems = gl.Problems.ToList();
            foreach (var problem in problems)
            {
                problemComboBox.Items.Add(problem.Name);
            }
            // Устанавливаем текущую проблему
            var selectedProblem = _selectedRequest.Problem.Name;
            problemComboBox.SelectedItem = selectedProblem;
        }

        // Метод для обработки нажатия на кнопку "Сохранить"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную проблему
            var selectedProblemName = problemComboBox.SelectedItem.ToString();
            var problem = gl.Problems.FirstOrDefault(p => p.Name == selectedProblemName);

            if (problem == null)
            {
                MessageBox.Show("Ошибка: Проблема не найдена.");
                return;
            }

            // Обновляем данные заявки
            _selectedRequest.Model = modelTextBox.Text;
            _selectedRequest.Problem = problem;

            // Сохраняем изменения в базе данных
            gl.SaveChanges();

            MessageBox.Show("Заявка успешно обновлена!");
            this.Close();
        }
    }
}
