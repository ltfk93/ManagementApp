using System.Collections.Generic;
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
        KeyValuePair<string, string> sortOrder = new KeyValuePair<string, string>("PersonID", "asc");
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
        public DatabaseContent(int userID, string userName)
        {
            this.userID = userID;
            this.userName = userName;
            InitializeComponent();
            seedList(sortOrder);
        }
        private void listViewColumn_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            switch(column.Tag.ToString().ToLower())
            {
                case "firstname":
                    {
                        if (sortOrder.Key.Equals("p.Firstname"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Firstname", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Firstname", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "lastname":
                    {
                        if (sortOrder.Key.Equals("p.Lastname"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Lastname", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Lastname", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "age":
                    {
                        if (sortOrder.Key.Equals("p.Age"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Age", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.Age", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "totalincome":
                    {
                        if (sortOrder.Key.Equals("p.TotalIncome"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.TotalIncome", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.TotalIncome", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "retirementfund":
                    {
                        if (sortOrder.Key.Equals("p.RetirementFunt"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.RetirementFunt", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.RetirementFunt", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "handler":
                    {
                        if (sortOrder.Key.Equals("l.Workername"))
                        {
                            sortOrder = new KeyValuePair<string, string>("l.Workername", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("l.Workername", "asc");
                        }
                        peopleDatabase.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                default:
                    MessageBox.Show($"Clicked on {column.Tag.ToString()}");
                    break;
            }
        }
        public void seedList(KeyValuePair<string, string> sortOptions)
        {
            string query = 
                $"SELECT p.PersonID, p.FirstName, p.LastName, p.Age, p.PictureUrl, p.TotalIncome, p.RetirementFunt, l.Workername From Person as p LEFT JOIN Logintable as l ON p.EmployeeID = l.EmployeeID ORDER BY {sortOptions.Key} {sortOptions.Value}";

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
                    seedList(sortOrder);
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
