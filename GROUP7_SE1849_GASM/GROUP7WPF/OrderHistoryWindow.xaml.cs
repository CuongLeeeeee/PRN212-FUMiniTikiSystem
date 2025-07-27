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
using FUMiniTikiSystem.BLL.Interfaces;
using FUMiniTikiSystem.BLL.Services;
using FUMiniTikiSystem.DAL.Repositories;
using FUMiniTikiSystem.DAL;

namespace GROUP7WPF
{
    /// <summary>
    /// Interaction logic for OrderHistoryWindow.xaml
    /// </summary>
    public partial class OrderHistoryWindow : Window
    {
        private readonly IOrderService _orderService;

        public OrderHistoryWindow()
        {
            InitializeComponent();

            // Khởi tạo service (nếu không dùng DI container)
            var context = new FuminiTikiSystemContext();
            var repo = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context);
            _orderService = new OrderService(repo, unitOfWork);

            Loaded += OrderHistoryWindow_Loaded;
        }

        private async void OrderHistoryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadOrders();
        }

        private async Task LoadOrders()
        {
            var orders = await _orderService.GetAllAsync();
            lvOrders.ItemsSource = orders;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
