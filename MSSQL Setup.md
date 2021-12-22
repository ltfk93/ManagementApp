## Guide to set up the tables for the application

#NB: If you are going to use MSSQL with this project, please remember to change all the variables that are an instance of SQLiteConnection and SQLiteCommand to SqlConnection and SqlCommand respectively.

To run this program with MSSQL, you need to install both an instance of a mssql server(SQL Server 2019 is a free edition), and Microsoft SQL Server Management Studio to interact with the MSSQL server. Follow this guide to download and set it up: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15


1. Install SQL Server 2019 Express: https://www.microsoft.com/en-us/sql-server/sql-server-downloads.
   Install Microsoft SQL Server Management Studio: 

2. Get the server name and username/password from the login window in MSSQL. Password is not required by default when using Windows authentication:
<img width="365" alt="image" src="https://user-images.githubusercontent.com/68993851/146932668-7e6d803e-a86e-4b27-a71c-19ddb649e700.png">

3. Right-click on Databases and create a new database.

4. Expand the Databases folder, right click on the database you created and click "New Query"

5. Paste in the following queries to create both tables and insert values. Remember to execute the query(Marked with yellow in screenshot):

<b></i>CREATE TABLE Person(
	PersonID int IDENTITY(1,1) PRIMARY KEY, Firstname VARCHAR(255), Lastname VARCHAR(255), Age int NOT NULL, EmployeeID int NOT NULL,
	PictureUrl VARCHAR(255), Notes VARCHAR(5000), TotalIncome int NOT NULL, Retirementfund int NOT NULL
);

Insert INTO Person(Firstname, Lastname, Age, EmployeeID, PictureUrl, Notes, TotalIncome, Retirementfund) VALUES(
	'Abel','Hansen',41,1, 'Pictures\1.png', '', 4300000, 215000),
	('Abdi','Fatah',33,1, 'Pictures\2.png', '', 4300000, 215000),
	('Jonas','Støhre',46,2, 'Pictures\3.png', '', 4300000, 215000),
	('Erna','Solberg',42,2, 'Pictures\4.png', '', 4300000, 215000),
	('Kristian','Mikkelsen',50,1, 'Pictures\5.png', '', 4300000, 215000),
	('Sigurd','Fossbakken',19,1, 'Pictures\6.png', '', 4300000, 215000),
	('Alfred','Løken',41,2, 'Pictures\7.png', '', 4300000, 215000),
	('Henrik','Ibsen',71,1, 'Pictures\8.png', '', 4300000, 215000),
	('Ola','Normann',91,1, 'Pictures\9.png', '', 4300000, 215000),
	('Kristoffer','Iversen',28,2, 'Pictures\10.png', '', 4300000, 215000),
	('Mohammed','Al-sharai',36,2, '', '', 4300000, 215000),
	('Elias','Rødstøl',28,2, '', '', 4300000, 215000),
	('Ahmed','Bainz',24,2, '', '', 4300000, 215000),
	('Kathrine','Andersen',31,2, '', '', 4300000, 215000),
	('Elise','Hoksrød',21,2, '', '', 4300000, 215000),
	('Sarah','White',41,1, '', '', 4300000, 215000),
	('Hasna','Jarlsberg',22,1, '', '', 4300000, 215000),
	('Aleksander','Espenakk',41,1, '', '', 4300000, 215000),
	('Ali','Beriani',41,1, '', '', 4300000, 215000),
	('Finn','Oddvar',44,2, '', '', 4300000, 215000),
	('Petter','Realsen',37,2, '', '', 4300000, 215000),
	('Magnus','Tonsen',39,2, '', '', 4300000, 215000),
	('Farook','Hansen',21,2, '', '', 4300000, 215000),
	('Jørgen','Andersen',41,1, '', '', 4300000, 215000),
	('Bjørn','Gåserud',48,1, '', '', 4300000, 215000),
	('Lena','Halvorsen',31,1, '', '', 4300000, 215000),
	('Borgar','Nordsveen',24,1, '', '', 4300000, 215000),
	('Joakim','Andersen',33,1, '', '', 4300000, 215000),
	('Kristian','Løken',36,2, '', '', 4300000, 215000),
	('Stine','Pettersen',48,1, '', '', 4300000, 215000
);


CREATE TABLE LoginTable(
	EmployeeID int IDENTITY(1,1) PRIMARY KEY, Username VARCHAR(50) NOT NULL, Password VARCHAR(50) NOT NULL, Workername VARCHAR(150) NOT NULL
);

INSERT INTO LoginTable(Username, Password, Workername) VALUES(
'admin','admin','Administrator'),
('user','password','Default User'
);</i></b>

<img width="1142" alt="image" src="https://user-images.githubusercontent.com/68993851/146937257-0a90ca26-43d1-4543-a37f-820ad3546fea.png">



6. Make sure that you set the correct values in the DBClass for Servername, Databasename, Username and password so that the connectionstring to the server is made.

