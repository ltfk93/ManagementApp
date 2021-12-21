using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using System.Collections.Generic;

namespace ManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This class is the meat of the application.Works as a userinterface for the worker.
    /// </summary>
    public partial class MainWindow : Window
    {

        //Creating a key/value pair that will store which columnheader that the list should sort by as "key", and the direction as "value"
        KeyValuePair<string, string> sortOrder = new KeyValuePair<string, string>("PersonID", "asc");
       
        //Setting connectionstring to MSSQL server. Also setting a default path for the picture that will show for users that has no pictures saved
        const string defaultPic = @"Pictures\none.png";
        string connectionString = DBClass.getConnectionString();

        //Declaring the SQL variables
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        //Creating Stringbuilders that will hold the temporary text values of Firstname, Lastname and Notes
        StringBuilder tempFirstName = new StringBuilder();
        StringBuilder tempLastName = new StringBuilder();
        StringBuilder tempNotes = new StringBuilder();

        //Creating int variables that will hold the temporary values for Age and TotalIncome.
        int tempAge = 0;
        int tempTotalEarned = 0;

        //Storing the PersonID of the selected person from the listview in the int variable personID. This is used to make changes to the user in a SQL Query 
        int personID = -1;

        //Storing the EmployeeID and Workername in these variables.
        int userID;
        string userName;

        public MainWindow(string username, int userID)
        {
            this.userID = userID;
            this.userName = username;
            InitializeComponent();

            //Creating the logout button
            Button logOutbutton = new Button();
            logOutbutton.Content = "Log Out";
            logOutbutton.Click += logOutBtn_Click;
            logOutbutton.Width = 120;

            //Creating the button to load all people from the database
            Button loadMoreButton = new Button();
            loadMoreButton.Content = "Load database";
            loadMoreButton.Width = 120;
            loadMoreButton.Click += loadDatabaseListWindow;

            //Creating the button to refresh the listview
            Button refreshList = new Button();
            refreshList.Content = "Refresh List";
            refreshList.Width = 120;
            refreshList.Click += seedList;

            //Creating label to display the user that is logged in.
            Label userIDLabel = new Label();
            userIDLabel.Content = $"Logged in as {username}";

            //Creating a stackpanel that will be aligned to the right. It will hold 2 buttons.
            StackPanel spRight = new StackPanel();
            spRight.Children.Add(loadMoreButton);
            spRight.Children.Add(refreshList);
            spRight.HorizontalAlignment = HorizontalAlignment.Right;

            //Creating a stackpanel that will be aligned to the left. It will hold a label and logout button.
            StackPanel spLeft = new StackPanel();
            spLeft.Children.Add(userIDLabel);
            spLeft.Children.Add(logOutbutton);
            spLeft.HorizontalAlignment = HorizontalAlignment.Left;

            //Adding both stackpanels to a Dockpanel in Mainwindow.xaml .
            topDock.Children.Add(spLeft);
            topDock.Children.Add(spRight);

            seedList(sortOrder);
        }

        //Function that runs if the user clicks on the edit button.
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if an entry in the listview has been selected. If not, nothing will happen when clicking on edit.
            if(peopleList.SelectedItems.Count != 0)
            {
                int validAge;
                int validFund;
               
                //Checking if the content value of editBtn. If it is edit, it changes the button text to "save" and makes the cancel button visible.
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
                    if (!int.TryParse(ageField.Text, out validAge))
                    {
                        MessageBox.Show("Invalid value in age field. Please type in only numbers.");
                        return;
                    }
                    else if (!int.TryParse(earnedField.Text, out validFund))
                    {
                        MessageBox.Show("Invalid value in Total income field. Please type in only numbers.");
                        return;
                    }
                    //Checks if there was no changes made
                    if ((firstNameField.Text.ToLower().Equals(tempFirstName.ToString().ToLower()) && lastNameField.Text.ToLower().Equals(tempLastName.ToString().ToLower()) && tempAge == 
                        int.Parse(ageField.Text.Length < 1 ? "0" : ageField.Text) && tempTotalEarned == int.Parse(earnedField.Text.ToString().Length < 1 ? "0" : earnedField.Text.ToString())) && tempNotes.ToString().Equals(notesBox.Text))
                    {
                        MessageBox.Show("No changes were made.", "Information");
                        clearTemps();
                        makeUnEditable();
                    }

                    //Checks if user is trying to save one of the fields with no information.
                    else if(firstNameField.Text.Length < 1 || lastNameField.Text.Length < 1 || ageField.Text.Length < 1 || earnedField.Text.Length < 1)
                    {
                        MessageBox.Show("You have to fill out all the fields to make a change. Reverting changes and canceling edit.","Information");

                        firstNameField.Text = tempFirstName.ToString();
                        lastNameField.Text = tempLastName.ToString();
                        ageField.Text = tempAge.ToString();
                        earnedField.Text = tempTotalEarned.ToString();

                        clearTemps();
                        makeUnEditable();
                    }
                    //Changes were detected. Calling the changePerson function with the changes that will be queried in to the database.
                    else
                    {
                        changePerson(firstNameField.Text, lastNameField.Text, int.Parse(ageField.Text), int.Parse(earnedField.Text), 
                           tempNotes.ToString().Equals(notesBox.Text) ? "" : notesBox.Text, personID);
                        MessageBox.Show("Changes has been made successfully. Reloading list.");
                        peopleList.Items.Clear();

                        firstNameField.Text = string.Empty;
                        lastNameField.Text = string.Empty;
                        ageField.Text = string.Empty;
                        earnedField.Text = string.Empty;
                        fundField.Text = string.Empty;
                        notesBox.Text = string.Empty;
                        imageProfile.Source = new BitmapImage(new Uri(defaultPic, UriKind.Relative));
                        imageProfile.Width = 300;
                        imageProfile.Height = 300;

                        clearTemps();
                        makeUnEditable();
                        seedList(sortOrder);
                    }
                }
            }
        }

        //This functions runs when the log out button is clicked. Closes the MainWindow and opens the login window again.
        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        //Function to populate the listview. Takes a Key/Value pair for options on what to order the list by, and in which direction.
        public void seedList(KeyValuePair<string,string> sortOptions)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * From Person WHERE EmployeeID={userID}";
            command.CommandText += $" ORDER BY {sortOptions.Key} {sortOptions.Value}"; 


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

        //Seedlist overload function created so that the button to refresh the list can call it.
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

        //Function to change the person in the database.
        public void changePerson(string FirstName, string LastName, int age, int totalEarned, string note, int ID)
        {
            int retirement = (totalEarned * 5) / 100;

            //Checks if something was passed in to the note. It will be an empty string if there was no changes made to the notes. If there is a string passed, it gets appended to the existing notes the person has,
            tempNotes.Append(note.Length < 1 ? "" : $"New note:\n{note}");
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = 
                $"UPDATE Person Set Firstname = '{FirstName}', Lastname = '{LastName}', Age = '{age}', TotalIncome = '{totalEarned}', Retirementfund = '{retirement}', Notes = '{tempNotes.ToString()}' WHERE PersonID = {ID}";

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        //This function runs when a person from the listview has been clicked on. Populates the fields and sets the image if there is an image path saved.
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

                imageProfile.Source = new BitmapImage(new Uri(itemFromList.PictureUrl, UriKind.Relative));
                imageProfile.Width = 300;
                imageProfile.Height = 300;
                imageProfile.HorizontalAlignment = HorizontalAlignment.Right;

                personID = itemFromList.ID;
            }
            else
                return;

        }

        //This function runs when a column header has been clicked on. Sorts the list depending on what the tag of the columnheader is, and what the key/value pair for that tag is.
        private void listViewColumn_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            switch (column.Tag.ToString().ToLower())
            {
                case "firstname":
                    {
                        if(sortOrder.Key.Equals("Firstname"))
                        {
                            sortOrder = new KeyValuePair<string, string>("Firstname", sortOrder.Value == "asc" ? "desc" : "asc");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("Firstname","asc");
                        }
                        peopleList.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "lastname":
                    {
                        if (sortOrder.Key.Equals("Lastname"))
                        {
                            sortOrder = new KeyValuePair<string, string>("Lastname", sortOrder.Value == "asc" ? "desc" : "asc");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("Lastname", "asc");
                        }
                        peopleList.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "age":
                    {
                        if (sortOrder.Key.Equals("Age"))
                        {
                            sortOrder = new KeyValuePair<string, string>("Age", sortOrder.Value == "asc" ? "desc" : "asc");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("Age", "asc");
                        }
                        peopleList.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "totalincome":
                    {
                        if (sortOrder.Key.Equals("TotalIncome"))
                        {
                            sortOrder = new KeyValuePair<string, string>("TotalIncome", sortOrder.Value == "asc" ? "desc" : "asc");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("TotalIncome", "asc");
                        }
                        peopleList.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                case "retirementfund":
                    {
                        if (sortOrder.Key.Equals("RetirementFund"))
                        {
                            sortOrder = new KeyValuePair<string, string>("RetirementFund", sortOrder.Value == "asc" ? "desc" : "asc");
                        }
                        else
                        {
                            sortOrder = new KeyValuePair<string, string>("RetirementFund", "asc");
                        }
                        peopleList.Items.Clear();
                        seedList(sortOrder);
                        break;
                    }
                default:
                    MessageBox.Show($"Clicked on {column.Tag.ToString()}");
                    break;
            }
        }

        //This function runs when user clicks on Load database button. Opens a new instance of DatabaseContent, and passes in the EmployeeID and Workername.
        private void loadDatabaseListWindow(object sender, RoutedEventArgs e)
        {
            new DatabaseContent(userID, userName).Show();
        }

        //Makes all the fields readonly and changes the background color for the textbox`es
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

        //Makes all the fields editable and changes the background color for the textbox`es
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

        //This function runs if the cancel button has been clicked. Disables editing on the selected person from the list and runs makeUnEditable().
        public void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            firstNameField.Text = tempFirstName.ToString();
            lastNameField.Text = tempLastName.ToString();
            ageField.Text = tempAge.ToString();
            earnedField.Text = tempTotalEarned.ToString();
            notesBox.Text = tempNotes.ToString();

            tempFirstName.Clear();
            tempLastName.Clear();
            tempTotalEarned = 0;
            tempAge = 0;
            tempNotes.Clear();

            makeUnEditable();
        }

        //Clears the temporary values
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
