using DataProtocol;
using System.Data.SqlClient;

namespace DAL
{
    public class sqlAdapater
    {
        public SqlConnection connectToSQL() {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=IceCreamCoffeShop;Trusted_Connection=True;";
                //builder.DataSource = @"localhost\SQLEXPRESS"; // INSERT HERE CORRECT SETTINGS
                //builder.InitialCatalog = "master";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public Boolean createDatabase() {
            SqlConnection connection = connectToSQL();
            connection.Open();
            String sql;
            SqlCommand command;
            // -------- CREATE DATABASE -------- //
            try {
            sql = "CREATE DATABASE IceCreamCoffeShop;";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Database created!");
            } 
            catch 
            {
                Console.WriteLine("Database already exists..");
            }
            return true;
        }
        public Boolean createTables() {
            // -------- CREATE TABLES -------- //
            SqlConnection connection = connectToSQL();
            connection.Open();
            String sql;
            SqlCommand command;
            try {
            sql =
                "DROP TABLE IF EXISTS sales;"+
                "CREATE TABLE sales ("+
                "Sid INT NOT NULL PRIMARY KEY IDENTITY(1,1),"+
                "Date VARCHAR(45) NOT NULL,"+
                "Sum INT NULL);";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

            sql = 
                "DROP TABLE IF EXISTS Portions;"+
                "CREATE TABLE Portions "+
                "(Pid int NOT NULL PRIMARY KEY IDENTITY(1,1),"+
                "Iid int NOT NULL,"+
                "Sid int NOT NULL,"+
                "amount int NOT NULL);";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

            sql = 
                "DROP TABLE IF EXISTS Ingredients;"+
                "CREATE TABLE Ingredients ("+
                "Iid int NOT NULL PRIMARY KEY IDENTITY(1,1),"+
                "Name VARCHAR(45) NOT NULL,"+
                "Price int NOT NULL);";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            } 
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Tables created!");
            return true;
        }

        public Boolean editSale(Sale newsale)
        {
            if (newsale.Id == 0) { // This is a brand new sale
                String sql = $"INSERT INTO sales(Date) OUTPUT Inserted.Sid VALUES('{newsale.date}');";
                            
                try {
                    SqlConnection connection = connectToSQL();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    //command.ExecuteNonQuery();
                    //command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) // READ THE SQL ANSWER HERE
                    { 
                        var Sid = reader.GetInt32(0);
                        newsale.Id = Sid;
                        Console.WriteLine("Added a new sale to the SQL server!, sid: "+Sid.ToString());
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            } else { // This is an existing sale that is being edited
                String sql = $"UPDATE QUEERY WIP"; // THIS SECTION IS WIP
                try {
                    SqlConnection connection = connectToSQL();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    //SqlDataReader reader = command.ExecuteReader();
                    //reader.Read();
                    //var Sid = reader.GetInt32(1);
                    //var Date = reader.GetString(2);
                    Console.WriteLine("Added a new sale to the SQL server!, sid: ");
                    
                }    
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return true;
        }

        public Sale getSale() {
            Sale ResultSale = new Sale();
            try
            {
                using (SqlConnection connection = connectToSQL())
                {
                    connection.Open();

                    String sql = $"insert sql querry here"; // INSERT SQL QUERRY HERE

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) // READ THE SQL ANSWER HERE
                            {
                                ResultSale.Id = reader.GetInt32(1);
                                ResultSale.date = reader.GetString(2);
                                ResultSale.TotalPrice = reader.GetFloat(3);
                            }
                            
                        }
                    }
                    connection.Close(); // NOT SURE IF NEEDED
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return ResultSale;
        }

        

    }
}