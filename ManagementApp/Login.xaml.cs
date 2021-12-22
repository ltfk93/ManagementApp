using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows;

namespace ManagementApp
{
    /// <summary>
    /// Login window for the Management App application. Connects to a MSSQL Database, and checks if the username and password typed in corresponds to an entry in the LoginTable.
    /// 
    /// LoginTable has the following fields: EmployeeID(Primary key) int not null, Username varchar(50) not null, Password varchar(50) not null, Workername varchar(100) not null
    /// </summary>
    public partial class Login : Window
    {
        //Connectionstring for the MSSQL database
        string connectionString = DBClass.getConnectionString();


        //Declaring variables that will connect to the database and query/read result.
        SQLiteConnection connection;
        SQLiteCommand command;

        public Login()
        {
            InitializeComponent();
        }

        //This function runs when the Login button has been clicked. 
        //The function first checks if the username and password fields has been filled out. If not, it will show a message box informing the user about the mistake.
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (usernameField.Text.Length < 1 || passswordField.ToString().Length < 1)
            {
                MessageBox.Show("Please fill out both a username and password.", "Error");
            }
            else
            {
                //These 2 variables will hold the EmployeeID and the Workername for the user logging in.
                int userID = -1;
                string username = string.Empty;

                //Initializing the sql variables.
                connection = new SQLiteConnection(connectionString);
                connection.Open();

                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * from LoginTable";
                var reader = command.ExecuteReader();


                //Creating a bool variable that will either be true if there is a match of an entry having same username(not case-sensitive) and password, or false otherwise.
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

                //Manual garbagecollection
                connection.Close();
                command.Dispose();
                //reader.Close();
                connection.Dispose();

                if (!validLogin)
                {
                    MessageBox.Show("Login failed. Please check your username and password.", "Error");
                }
                else
                {
                    //A match was found in the LoginTable. Showing a message box to greet the user with his worker name, creating a new instance of the MainWindow and closing the login window.
                    MessageBox.Show($"Welcome {username}", "Login success");
                    MainWindow mainWindow = new MainWindow(username, userID);
                    mainWindow.Show();
                    this.Close();
                }
            }

        }
        //This method runs when the Cancel button has been clicked. Closes down the login window.
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting program", "Shutting down");
            this.Close();
        }
    }
}
