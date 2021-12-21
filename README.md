# ManagementApp
This application is a management application with MSSQL as the SQL Server. To make this application executable, you need to create a Database with MSSQL and create 2 tables: Person and LoginTable.

The LoginTable should contain the "admins" that are going to manage people from the people database. This table has 4 columns: EmployeeID INT(Autoincrement/Primary key), Username(Varchar(50)), Password(Varchar(50)), Workername(Varchar(150)).

The Person table contains all of the people that are registered in the system. This table has 9 columns: PersonID INT(Autoincrement/Primary key), Firstname(Varchar(50)), LastName(Varchar(50)), Age(INT), EmployeeID(INT), PictureUrl(Varchar(255)), Notes(Varchar(5000)), TotalIncome(INT), RetirementFund(INT)

The EmployeeID in the <b>Person</b> table is used to assign it a handler from the <b>Logintable</b>. The assigned handler has the same employeeID.

To set up the SQL server and tables, please follow the guide <a href="https://github.com/ltfk93/ManagementApp/blob/master/MSSQL%20Setup.md">MSSQL Setup.md</a>

After that is done, you should be able to use the program and have seeded data in the database. The project can be downloaded as a zip:
![image](https://user-images.githubusercontent.com/68993851/146937997-7422247e-afc3-4614-85b2-cb2998977a07.png)

You can log on with 2 users: 

<b>Username: admin  &emsp;   Password: admin<br>
Username: user  &emsp;    Password: password</b>

Note that the username is not case-sensitive, but the password is.
