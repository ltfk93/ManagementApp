using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ManagementApp
{
    /// <summary>
    /// Interaction logic for DatabaseContent.xaml
    /// </summary>
    public partial class DatabaseContent : Window
    {
        ListView lw;
        private int userID;
        private string userName;
        const string defaultPic = @"C:\Users\Ali\Pictures\none.png";
        const string connectionString = @"Data Source=DESKTOP-0M7SFEP\SQLEXPRESS;
                                        Initial Catalog=PersonDatabase;
                                        User ID=desktop-0m7sfep\ali;
                                        Password=;
                                        Trusted_Connection=Yes";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public DatabaseContent(ListView lw, int userID, string userName)
        {
            this.lw = lw;
            this.userID = userID;
            this.userName = userName;
            InitializeComponent();
            seedList();
        }
        private void listViewColumn_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            switch(column.Tag.ToString().ToLower())
            {
                case "firstname":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("p.Firstname");
                        break;
                    }
                case "lastname":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("p.Lastname");
                        break;
                    }
                case "age":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("p.Age");
                        break;
                    }
                case "totalincome":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("p.TotalIncome");
                        break;
                    }
                case "retirementfund":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("p.RetirementFunt");
                        break;
                    }
                case "handler":
                    {
                        peopleDatabase.Items.Clear();
                        seedList("l.Workername");
                        break;
                    }
                default:
                    MessageBox.Show($"Clicked on {column.Tag.ToString()}");
                    break;
            }
        }
        public void seedList(string commandText = "")
        {
            string query = $"SELECT p.PersonID, p.FirstName, p.LastName, p.Age, p.PictureUrl, p.TotalIncome, p.RetirementFunt, l.Workername From Person as p LEFT JOIN Logintable as l ON p.EmployeeID = l.EmployeeID";
            
            if (commandText.Length > 0)
            {
                query += $" ORDER BY {commandText}";
            }

            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                peopleDatabase.Items.Add(new Person()
                {
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    ID = reader.GetInt32(0),
                    PictureUrl = reader.GetString(4).Length > 1 ? reader.GetString(4) : defaultPic,
                    TotalIncome = reader.GetInt32(5),
                    RetirementFund = reader.GetInt32(6),
                    Handler = reader.GetString(7)
                });
            }

            closeConnections();
        }

        private void manageBtn_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = (Person) peopleDatabase.SelectedItems[0];
            if(selectedPerson.Handler != null && !selectedPerson.Handler.Equals(userName))
            {
                System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show($"{selectedPerson.FirstName} {selectedPerson.LastName} already has a handler\nAre you sure you want to take over the handler role?", "Warning",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if(dr == System.Windows.Forms.DialogResult.Yes)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"UPDATE Person SET EmployeeID = {userID} WHERE PersonID = {selectedPerson.ID}";
                    command.ExecuteNonQuery();
                    closeConnections();
                    peopleDatabase.Items.Clear();
                    seedList();
                }
            }

        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void peopleDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(peopleDatabase.SelectedItems.Count != 0)
            {
                Person selectedPerson = (Person)peopleDatabase.SelectedItems[0];
                if(selectedPerson.Handler.Equals(userName))
                {
                    manageBtn.Visibility = Visibility.Hidden;
                }
                else
                {
                    manageBtn.Visibility = Visibility.Visible;
                }
            }
            else
            {
                manageBtn.Visibility = Visibility.Hidden;
            }
        }
        private void closeConnections()
        {
            reader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
