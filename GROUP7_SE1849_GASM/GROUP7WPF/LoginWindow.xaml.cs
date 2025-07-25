using System.Windows;
using FUMiniTikiSystem.BLL.Services;
using FUMiniTikiSystem.DAL.Repositories;
using FUMiniTikiSystem.DAL;

namespace GROUP7WPF
{
    public partial class LoginWindow : Window
    {
        private readonly CustomerService _customerService;

        public LoginWindow()
        {
            InitializeComponent();

            var dbContext = new FuminiTikiSystemContext();
            var repo = new CustomerRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            _customerService = new CustomerService(repo, unitOfWork);
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var customer = await _customerService.LoginAsync(email, password);
            if (customer != null)
            {
                bool isAdmin = (customer.Email == "admin@gmail.com"); // tuỳ logic
                var catalogWindow = new ProductCatalogWindow(isAdmin);
                catalogWindow.Closed += (s, args) => this.Close();
                catalogWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
