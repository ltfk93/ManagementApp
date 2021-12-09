using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;

namespace ManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string defaultPic = @"C:\Users\Ali\Pictures\none.png";
        const string connectionString = @"Data Source=DESKTOP-0M7SFEP\SQLEXPRESS;
                                        Initial Catalog=PersonDatabase;
                                        User ID=desktop-0m7sfep\ali;
                                        Password=;
                                        Trusted_Connection=Yes";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        StringBuilder tempFirstName = new StringBuilder();
        StringBuilder tempLastName = new StringBuilder();
        StringBuilder tempNotes = new StringBuilder();
        int tempAge = 0;
        int tempTotalEarned = 0;

        int personID = -1;
        int userID;
        string userName;
        public MainWindow(string username, int userID)
        {
            this.userID = userID;
            this.userName = username;
            InitializeComponent();


            Button logOutbutton = new Button();
            logOutbutton.Content = "Log Out";
            logOutbutton.Click += logOutBtn_Click;
            logOutbutton.Width = 120;

            Button loadMoreButton = new Button();
            loadMoreButton.Content = "Load database";
            loadMoreButton.Width = 120;
            loadMoreButton.Click += loadDatabaseListWindow;

            Button refreshList = new Button();
            refreshList.Content = "Refresh List";
            refreshList.Width = 120;
            refreshList.Click += seedList;

            StackPanel spRight = new StackPanel();
            spRight.Children.Add(loadMoreButton);
            spRight.Children.Add(refreshList);
            spRight.HorizontalAlignment = HorizontalAlignment.Right;

            Label userIDLabel = new Label();
            userIDLabel.Content = $"Logged in as {username}";


            StackPanel spLeft = new StackPanel();
            spLeft.Children.Add(userIDLabel);
            spLeft.Children.Add(logOutbutton);
            spLeft.HorizontalAlignment = HorizontalAlignment.Left;

            topDock.Children.Add(spLeft);
            topDock.Children.Add(spRight);

            seedList();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if(peopleList.SelectedItems.Count != 0)
            {
                if (editBtn.Content.Equals("Edit"))
                {

                    makeEditable();

                    tempFirstName.Append(firstNameField.Text);
                    tempLastName.Append(lastNameField.Text);
                    tempAge = int.Parse(ageField.Text);
                    tempTotalEarned = int.Parse(earnedField.Text);
                    tempNotes.Append(notesBox.Text);
                    
                }
                else
                {
                    if ((firstNameField.Text.ToLower().Equals(tempFirstName.ToString().ToLower()) && lastNameField.Text.ToLower().Equals(tempLastName.ToString().ToLower()) && tempAge == int.Parse(ageField.Text)
                        && tempTotalEarned == int.Parse(earnedField.Text.ToString())) && tempNotes.ToString().Equals(notesBox.Text))
                    {
                        MessageBox.Show("No changes were made.", "Information");
                        clearTemps();
                        makeUnEditable();
                    }
                    else if(firstNameField.Text.Length < 1 || lastNameField.Text.Length < 1 || ageField.Text.Length < 1 || earnedField.Text.Length < 1)
                    {
                        MessageBox.Show("You have to fill out all the fields to make a change. Reverting changes and canceling edit.","Information");

                        firstNameField.Text = tempFirstName.ToString();
                        lastNameField.Text = tempLastName.ToString();
                        ageField.Text = tempAge.ToString();

                        clearTemps();
                        makeUnEditable();
                    }
                    else
                    {
                        changePerson(firstNameField.Text, lastNameField.Text, int.Parse(ageField.Text), int.Parse(earnedField.Text), notesBox.Text, personID);
                        MessageBox.Show("Changes has been made successfully. Reloading list.");
                        peopleList.Items.Clear();

                        firstNameField.Text = string.Empty;
                        lastNameField.Text = string.Empty;
                        ageField.Text = string.Empty;


                        clearTemps();
                        makeUnEditable();
                        seedList();
                    }
                }
            }
        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        public void seedList()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * From Person WHERE EmployeeID={userID}";

            reader = command.ExecuteReader();

            while(reader.Read())
            {
                peopleList.Items.Add(new Person() 
                { 
                    FirstName = reader.GetString(1), 
                    LastName = reader.GetString(2), 
                    Age = reader.GetInt32(3), 
                    ID = reader.GetInt32(0),
                    PictureUrl = reader.GetString(5).Length > 1 ? reader.GetString(5) : defaultPic, 
                    TotalIncome = reader.GetInt32(7), 
                    RetirementFund = reader.GetInt32(8),
                    Notes = reader.GetString(6)
                });
            }

        }
        public void seedList(object sender, RoutedEventArgs e)
        {
            peopleList.Items.Clear();
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * From Person WHERE EmployeeID={userID}";

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                peopleList.Items.Add(new Person()
                {
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    ID = reader.GetInt32(0),
                    PictureUrl = reader.GetString(5).Length > 1 ? reader.GetString(5) : defaultPic,
                    TotalIncome = reader.GetInt32(7),
                    RetirementFund = reader.GetInt32(8),
                    Notes = reader.GetString(6) == null ? "" : reader.GetString(6)
                });
            }

        }
        public void changePerson(string FirstName, string LastName, int age, int totalEarned, string note, int ID)
        {
            int retirement = (totalEarned * 5) / 100;

            tempNotes.Append($"New note:\n{note}");
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = 
                $"UPDATE Person Set Firstname = '{FirstName}', Lastname = '{LastName}', Age = '{age}', TotalIncome = '{totalEarned}', Retirementfunt = '{retirement}', Notes = '{tempNotes.ToString()}' WHERE PersonID = {ID}";

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
        private void peopleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (peopleList.SelectedItems.Count != 0)
            {
                Person itemFromList = (Person) peopleList.SelectedItems[0];
                firstNameField.Text = itemFromList.FirstName;
                lastNameField.Text = itemFromList.LastName;
                ageField.Text = itemFromList.Age.ToString();
                earnedField.Text = itemFromList.TotalIncome.ToString();
                fundField.Text = itemFromList.RetirementFund.ToString();
                notesBox.Text = itemFromList.Notes.ToString();

                imageProfile.Source = new BitmapImage(new Uri(itemFromList.PictureUrl));
                imageProfile.Width = 300;
                imageProfile.Height = 300;
                imageProfile.HorizontalAlignment = HorizontalAlignment.Right;

                personID = itemFromList.ID;
            }
            else
                return;

        }
        private void loadDatabaseListWindow(object sender, RoutedEventArgs e)
        {
            new DatabaseContent(peopleList, userID, userName).Show();
        }
        public void makeUnEditable()
        {
            firstNameField.IsReadOnly = true;
            firstNameField.Background = Brushes.LightGray;

            lastNameField.IsReadOnly = true;
            lastNameField.Background = Brushes.LightGray;

            ageField.IsReadOnly = true;
            ageField.Background = Brushes.LightGray;

            earnedField.IsReadOnly = true;
            earnedField.Background = Brushes.LightGray;

            notesBox.IsReadOnly = true;
            notesBox.Background = Brushes.LightGray;


            cancelBtn.Visibility = Visibility.Hidden;

            editBtn.Content = "Edit";
        }

        public void makeEditable()
        {
            firstNameField.IsReadOnly = false;
            firstNameField.Background = Brushes.White;

            lastNameField.IsReadOnly = false;
            lastNameField.Background = Brushes.White;

            ageField.IsReadOnly = false;
            ageField.Background = Brushes.White;

            earnedField.IsReadOnly = false;
            earnedField.Background = Brushes.White;

            notesBox.IsReadOnly = false;
            notesBox.Background = Brushes.White;


            cancelBtn.Visibility = Visibility.Visible;

            editBtn.Content = "Save";
        }

        public void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            tempFirstName.Clear();
            tempLastName.Clear();
            tempTotalEarned = 0;
            tempAge = 0;
            tempNotes.Clear();

            makeUnEditable();
        }
        public void clearTemps()
        {
            tempFirstName.Clear();
            tempLastName.Clear();
            tempTotalEarned = 0;
            tempAge = 0;
            tempNotes.Clear();
        }
    }
}
