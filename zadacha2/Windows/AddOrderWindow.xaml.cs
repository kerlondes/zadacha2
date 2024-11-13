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
using Разработка_программного_модуля_для_учета_заявок_на_ремонт_оргтехники;

namespace zadacha2.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        BDEntities bd = new BDEntities();
        public AddOrderWindow()
        {
            InitializeComponent();
            LoadProblems();
            LoadTypes();
        }
        private void LoadProblems()
        {
            var problems = bd.Problems.ToList();
            foreach (var problem in problems)
            {
                problemComboBox.Items.Add(problem.Name);
            }
            if (problemComboBox.Items.Count > 0)
                problemComboBox.SelectedIndex = 0;
        }
        private void LoadTypes()
        {
            var types = bd.Orgtehnicas.ToList();
            foreach (var type in types)
            {
                TypeComboBox.Items.Add(type.Name);
            }
            if (TypeComboBox.Items.Count > 0)
                TypeComboBox.SelectedIndex = 0;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполненность поля "Модель"
            if (string.IsNullOrWhiteSpace(modelTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле 'Модель'.");
                return;
            }

            // Проверка на заполненность поля "Телефон"
            if (string.IsNullOrWhiteSpace(phon.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле 'Телефон'.");
                return;
            }

            // Проверка выбора в ComboBox для проблемы
            if (problemComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите описание проблемы.");
                return;
            }

            // Проверка выбора в ComboBox для типа оргтехники
            if (TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип оргтехники.");
                return;
            }

            // Получение выбранного описания проблемы
            var selectedProblemName = problemComboBox.SelectedItem.ToString();
            var problem = bd.Problems.FirstOrDefault(p => p.Name == selectedProblemName);
            if (problem == null)
            {
                MessageBox.Show("Ошибка: Проблема не найдена.");
                return;
            }

            // Получение выбранного типа оргтехники
            var selectedTypeName = TypeComboBox.SelectedItem.ToString();
            var type = bd.Orgtehnicas.FirstOrDefault(t => t.Name == selectedTypeName);
            if (type == null)
            {
                MessageBox.Show("Ошибка: Тип не найден.");
                return;
            }

            // Получение текущего клиента
            var currentClient = Session.currentUser;
            if (currentClient == null)
            {
                MessageBox.Show("Ошибка: Не найден текущий клиент.");
                return;
            }

            // Проверка изменения номера телефона клиента
            var phone = phon.Text;
            if (currentClient.Phone != phone)
            {
                currentClient.Phone = phone;
                bd.SaveChanges();
            }

            // Создание новой заявки
            var newRequest = new Order
            {
                Model = modelTextBox.Text,
                Problem = problem,
                Date = DateTime.Now,
                OrgId = type.ID,
                StatusID = 1,
                ClientId = currentClient.ID
            };

            bd.Orders.Add(newRequest);
            bd.SaveChanges();

            MessageBox.Show("Заявка успешно добавлена!");
            this.Close();
        }
    }
}
