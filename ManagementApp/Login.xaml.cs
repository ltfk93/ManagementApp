using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ManagementApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string connectionString = @"Data Source=DESKTOP-0M7SFEP\SQLEXPRESS;
                                        Initial Catalog=PersonDatabase;
                                        User ID=desktop-0m7sfep\ali;
                                        Password=;
                                        Trusted_Connection=Yes";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(usernameField.Text.Length < 1 || passswordField.ToString().Length < 1)
            {
                MessageBox.Show("Please fill out both a username and password.", "Error");
            }
            else
            {
                int userID = -1;
                string username = string.Empty;
                connection = new SqlConnection(connectionString);
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * from LoginTable";

                reader = command.ExecuteReader();

                bool validLogin = false;
                while (reader.Read())
                {
                    if (reader.GetString(1).ToLower().Equals(usernameField.Text.ToLower()) && reader.GetString(2).Equals(passswordField.Password.ToString()))
                    {
                        userID = reader.GetInt32(0);
                        username = reader.GetString(3);
                        validLogin = true;
                    }
                }
                connection.Close();

                command.Dispose();
                reader.Close();
                connection.Dispose();

                if(!validLogin)
                {
                    MessageBox.Show("Login failed. Please check your username and password.", "Error");
                }
                else
                {
                    MessageBox.Show($"Welcome {username}", "Login success");
                    MainWindow mainWindow = new MainWindow(username,userID);
                    mainWindow.Show();
                    this.Close();
                }
            }
            
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting program", "Shutting down");
            this.Close();
        }
    }
}
