# ManagementApp
This application is a management application with SQLite as the SQL Server. To make this application executable. The database in the SQlite server contains to tables: <b>Person</b> and <b>LoginTable</b>.

The LoginTable should contain the "admins" that are going to manage people from the people database. This table has 4 columns: EmployeeID INT(Autoincrement/Primary key), Username(Varchar(50)), Password(Varchar(50)), Workername(Varchar(150)).

The Person table contains all of the people that are registered in the system. This table has 9 columns: PersonID INT(Autoincrement/Primary key), Firstname(Varchar(50)), LastName(Varchar(50)), Age(INT), EmployeeID(INT), PictureUrl(Varchar(255)), Notes(Varchar(5000)), TotalIncome(INT), RetirementFund(INT)

The EmployeeID in the <b>Person</b> table is used to assign it a handler from the <b>Logintable</b>. The assigned handler has the same employeeID.

To set up the program with Microsoft SQL server instead, please follow the guide <a href="https://github.com/ltfk93/ManagementApp/blob/master/MSSQL%20Setup.md">MSSQL Setup.md</a>

The project can be downloaded by clicking Code -> Download ZIP:
![image](https://user-images.githubusercontent.com/68993851/146937997-7422247e-afc3-4614-85b2-cb2998977a07.png)

To run an executable of the program, run the "ManagementApp.exe" from <b>*Path_to_where_the_project_is_saved*\ManagementApp-master\ManagementApp\bin\Debug</b>

You can log on with 2 users: 

<b>Username: admin  &emsp;   Password: admin<br>
Username: user  &emsp;    Password: password</b>

Note that the username is not case-sensitive, but the password is.
