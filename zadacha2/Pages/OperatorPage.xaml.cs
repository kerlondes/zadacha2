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
    /// Логика взаимодействия для OperatorPage.xaml
    /// </summary>
    public partial class OperatorPage : Page
    {
        private BDEntities bd = new BDEntities();
        public OperatorPage()
        {
            InitializeComponent();
            LoadCompletedRequestsCount();
            LoadAverageCompletionTime();
            LoadProblemsStatistics();
        }
        // Загрузка количества выполненных заявок
        private void LoadCompletedRequestsCount()
        {
            int completedRequestsCount = bd.Orders.Count(z => z.StatusID == 3);
            completedRequestsTextBlock.Text = completedRequestsCount.ToString();
        }
        // Загрузка среднего времени выполнения заявки
        private void LoadAverageCompletionTime()
        {
            var completedRequests = bd.Orders
        .Where(z => z.StatusID == 3 && z.Date.HasValue && z.DateFin.HasValue)
        .ToList();
            var completionTimes = completedRequests
                .Select(z => (z.DateFin.Value - z.Date.Value).TotalDays)
                .ToList();
            double averageCompletionTime = 0;

            if (completionTimes.Count > 0) // Проверяем, есть ли элементы в списке
            {
                averageCompletionTime = completionTimes.Average(); // Если есть, вычисляем среднее
            }
            averageCompletionTimeTextBlock.Text = averageCompletionTime.ToString("0.00");
        }
        // Загрузка статистики по типам неисправностей
        private void LoadProblemsStatistics()
        {
            var problemStatistics = bd.Problems
                .Select(p => new
                {
                    ProblemName = p.Name,
                    RequestCount = p.Orders.Count() // Количество выполненных заявок по каждому типу неисправности
                })
                .ToList();

            problemsStatisticsDataGrid.ItemsSource = problemStatistics;
        }
    }
}
