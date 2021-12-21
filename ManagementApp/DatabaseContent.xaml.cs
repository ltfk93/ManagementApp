using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ManagementApp
{
    /// <summary>
    /// Interaction logic for DatabaseContent.xaml
    /// Class/Window to load in all of the people in the database.
    /// </summary>
    public partial class DatabaseContent : Window
    {

        public const string ServerName = @"DESKTOP-0M7SFEP\SQLEXPRESS";
        public const string TableName = "TestBase";
        public const string DBUsername = @"desktop-0m7sfep\ali";
        public const string DBPassword = "";


        //Creating a key/value pair that will store which columnheader that the list should sort by as "key", and the direction as "value"
        KeyValuePair<string, string> sortOrder = new KeyValuePair<string, string>("PersonID", "asc");

        //Setting connectionstring to MSSQL server. Also setting a default path for the picture that will show for users that has no pictures saved
        const string defaultPic = @"Pictures\none.png";
        string connectionString = $"Data Source={ServerName};Initial Catalog={TableName};User ID={DBUsername};Password={DBPassword};Trusted_Connection=Yes";

        //Declaring the SQL variables
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        //Storing the EmployeeID and Workername in these variables.
        private int userID;
        private string userName;

        public DatabaseContent(int userID, string userName)
        {
            this.userID = userID;
            this.userName = userName;
            InitializeComponent();
            seedList(sortOrder);
        }

        //This function runs when a column header has been clicked on. Sorts the list depending on what the tag of the columnheader is, and what the key/value pair for that tag is.
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
                        if (sortOrder.Key.Equals("p.RetirementFund"))
                        {
                            sortOrder = new KeyValuePair<string, string>("p.RetirementFund", sortOrder.Value == "asc" ? "desc" : "asc;");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("p.RetirementFund", "asc");
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

        //Function to populate the listview. Takes a Key/Value pair for options on what to order the list by, and in which direction.
        public void seedList(KeyValuePair<string, string> sortOptions)
        {
            string query = 
                $"SELECT p.PersonID, p.FirstName, p.LastName, p.Age, p.PictureUrl, p.TotalIncome, p.RetirementFund, l.Workername From Person as p LEFT JOIN Logintable as l ON p.EmployeeID = l.EmployeeID ORDER BY {sortOptions.Key} {sortOptions.Value}";

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

        //This function runs when the "Manage" button has been clicked. This is only clickable if the person chosen from the list is not already being handled/managed by the user logged in.
        private void manageBtn_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = (Person) peopleDatabase.SelectedItems[0];
            if(selectedPerson.Handler != null && !selectedPerson.Handler.Equals(userName))
            {
                //Creating a DialogResult variable to hold the respons from the message box. Promting the user to either click on yes or no whether the user is sure that he will take over the handler role for the user
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

        //This function runs when the Exit button is clicked. Closes this instance Window.
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //This function runs when a person from the listview has been clicked on. If the person chosen is already being handled by the user logged in, then the "Manage" button will be hidden. 
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

        //Closes the database connections and deals with garbagecollection.
        private void closeConnections()
        {
            reader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
