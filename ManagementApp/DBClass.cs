using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp
{
    //Class created to hold all the data for the SQL server, database name, username and password for the SQL server.
    public static class DBClass
    {
        public const string ServerName = @"DESKTOP-0M7SFEP\SQLEXPRESS";
        public const string Database = "TestBase";
        public const string DBUsername = @"desktop-0m7sfep\ali";
        public const string DBPassword = "";

        public static string getConnectionString()
        {
            return $"Data Source={ServerName};Initial Catalog={Database};User ID={DBUsername};Password={DBPassword};Trusted_Connection=Yes";
        }
    }
}
