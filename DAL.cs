using DataProtocol;
using System.Data.SqlClient;

namespace DAL
{
    public class sqlAdapater
    {
        /*
        This class is used to communicate with an SQL server. (Data-Access-Layer)
        Inorder to connect to your own SQL server, change the 'builder.ConnectionString' in the function 'connectToSQL()'.
        */
        public SqlConnection connectToSQL() {
            /* This function returns the connection to the SQL. It is used in almost every function in this class. 
            */
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=IceCreamCoffeShop;Trusted_Connection=True;";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            throw new ArgumentException("Couldn't establish connection with SQL server"); 
        }
        public Boolean createDatabase() {
            /*This function is used to create the database, it has to be used atleast once in the SQL server. 
            */ 
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
            /*This function is used to create the tables, it has to be used atleast once in the SQL server. 
            If used more than once, it will delete old records of sales. 
            */ 
            // -------- CREATE SALES AND PORTIONS TABLES -------- //
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


            } 
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Tables created!");
            return true;
        }

        public void loadIngredientsTable() {
            /*This function is used to load the Ingredients of the IceCreamShop into a LOOKUP TABLE that is stored in the SQL server.
            If the Ingredients of the shop were changed, the database manager has to call this function again to update the lookup table. 
            */ 
            // -------- CREATE INGREDIENT TABLE -------- //
            SqlConnection connection = connectToSQL();
            connection.Open();
            String sql;
            SqlCommand command;
            try {
                sql = 
                    "DROP TABLE IF EXISTS Ingredients;"+
                    "CREATE TABLE Ingredients ("+
                    "Iid int NOT NULL PRIMARY KEY IDENTITY(1,1),"+
                    "Name VARCHAR(45) NOT NULL"+
                    ");";
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                var values = Enum.GetValues(typeof(Taste));  // << This line gets the correct ingredient list from the DataProtocol
                foreach (var ingredientName in values) {
                    sql = $"INSERT INTO Ingredients(Name) VALUES('{ingredientName}');";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                values = Enum.GetValues(typeof(ExtraTaste)); // << This line gets the correct ingredient list from the DataProtocol
                foreach (var ingredientName in values) {
                    sql = $"INSERT INTO Ingredients(Name) VALUES('{ingredientName}');";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                values = Enum.GetValues(typeof(CupType)); // << This line gets the correct ingredient list from the DataProtocol
                foreach (var ingredientName in values) {
                    sql = $"INSERT INTO Ingredients(Name) VALUES('{ingredientName}');";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            } 
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            
            Console.WriteLine("Ingredient tables created!");
        }
        
        public Boolean editSale(Sale newsale)
        {
            /*
                Despite it's misleading name, this function is used to add a new sale to the database or to update an exisiting sale. 
                if the Sid of @variable 'newsale' is 0, it means that the sale is still not stored in the database. 
                Else, the sale is already stored in the database so the function will UPDATE the sale's information in the database. 
            */
            if (newsale.Sid == 0) { // This is a brand new sale
                String sql = $"INSERT INTO sales(Date) OUTPUT Inserted.Sid VALUES('{newsale.date}');";
                            
                try {
                    SqlConnection connection = connectToSQL();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) // READ THE SQL ANSWER HERE
                    { 
                        var Sid = reader.GetInt32(0);
                        newsale.Sid = Sid;
                        Console.WriteLine("Added a new sale to the SQL server!, sid: "+Sid.ToString());
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            } else { // This is an existing sale that is being edited
                String sql = $"UPDATE sales SET Sum = {newsale.TotalPrice} WHERE Sid = {newsale.Sid};"; 
                try {
                    SqlConnection connection = connectToSQL();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine(sql);
                }    
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return true;
        }

        public void recordPortionsOfSale(Sale currsale) {
            /*
                The 'Portions' table (as described in the ERD) is used to record the specific ingredients used in each sale. 
                Once the sale is finished and ready to be summed up, this function is used to record the ingredient of the sale in a designated table. ('Portions' table). 

            */
            int[] ingredientBucketArr = new int[16];
            foreach (var ball in currsale.Balls) {
                ingredientBucketArr[((int)ball.Taste)]++;
            }
            foreach (var extra in currsale.ExtrasOnBalls) {
                ingredientBucketArr[((int)extra.ExtraTaste)+10]++; // +10 to skip over the 10 ball tastes
            }
            ingredientBucketArr[((int)currsale.CupType)+13]++; // +13 to skip over the 10 ball tastes and 3 extras 
            try {
                for (int Iid = 1; Iid<=ingredientBucketArr.Length; Iid++) {
                    if (ingredientBucketArr[Iid-1]>0) { // then we need to add this portion of sale to the SQL
                        addIngredientToSale(currsale.Sid, Iid, ingredientBucketArr[Iid-1]);
                    }
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void addIngredientToSale(int Sid, int Iid, int amount) {
            /*
                Helper function for 'recordPortionsOfSale(Sale currsale)'.
            */
            String sql = $"INSERT INTO Portions(Iid, Sid, amount) VALUES({Iid}, {Sid}, {amount});";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

        }

        public string getReceipt(int Sid) {
            /*
                Uses an Sid to find the sale in the database and creates a receipt for it. 
            */
            string ans = ""; 
            String sql = $"SELECT * FROM sales WHERE Sid = {Sid};";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            var date = ""; var sum = 0;
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                date = reader.GetString(1);
                try 
                {
                    sum = reader.GetInt32(2);
                } 
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Can't return receipt of an unfinished sale!");
                }
            }
            reader.Close();
            ans += "Recipt for "+Sid+", "+date+", TOTAL: "+sum+"$\n";
            sql = $"SELECT * FROM Portions WHERE Sid = {Sid};";
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            var Iid = 0; var amount = 0;
            SqlConnection IngredientNameconnection = connectToSQL();
            IngredientNameconnection.Open();
            ans += "\t"+"Iid"+":\t"+"name"+",\t"+"amount"+"\n";
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                Iid = reader.GetInt32(1);
                amount = reader.GetInt32(3);
                sql = $"SELECT Name FROM Ingredients WHERE Iid = {Iid};";      
                Console.WriteLine(sql);
                SqlCommand IngredientCommand = new SqlCommand(sql, IngredientNameconnection);
                SqlDataReader IngredientNameReader = IngredientCommand.ExecuteReader();
                if (IngredientNameReader.Read()) {
                string name = IngredientNameReader.GetString(0);
                ans += "\t"+Iid+":\t"+name+",\t"+amount+"\n";
                } else {
                    throw new ArgumentException("Couldn't find Iid: "+ Iid);
                }
                IngredientNameReader.Close();
            }
            return ans;
        }

        public string unfinishedSales() {
            /*
                Returns all the unfinished sales in the database. 
                A sale is 'unfinished' if in the table 'sales', it's 'Sum' collum is null. (Should be an int representing the price)
            */
            string ans = "UNFINISHED SALES:\n";
            String sql = "SELECT * FROM sales WHERE sum IS NULL;";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                ans += "Sid: "+reader.GetInt32(0)+"\tDate: "+reader.GetString(1)+"\n";
            }
            return ans;
        }

        public void deleteUnfinishedSales() {
            /*
                Deletes all the sales that are not finished from the database. 
                A sale is 'unfinished' if in the table 'sales', it's 'Sum' collum is null. (Should be an int representing the price)
            */
            String sql = "DELETE FROM sales WHERE Sum IS NULL;";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
        public string getDaySum(string askedDate) {
            /*
                Returns a summary of sales occured in the askedDate. 
            */
            string ans = "";
            String sql = $"SELECT COUNT(Sum) FROM sales WHERE Date LIKE '{askedDate}%'";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            var amount = 0; var sum = 0; var avg = 0;
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                amount = reader.GetInt32(0);
            }
            reader.Close();
            sql =  $"SELECT SUM(Sum) FROM sales WHERE Date LIKE '{askedDate}%';";
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                sum = reader.GetInt32(0);
            }
            if (amount>0)
                avg = sum/amount;

            ans = "amount of sales: "+amount+"\nsum of sales: "+sum+"\n avarage: "+avg;
            return ans;
        }

        public string getBestSellers() {
            /*
                Returns the all time best sellers. Uses the Portion table to SUM (grouped by Iid). 
                Than, uses INNER JOIN on the Ingredients table (to look up) the name of the MAX SUM.
            */
            string ans = "";
            String sql = "SELECT Iid, Name, TotalAmount FROM Ingredients INNER JOIN (SELECT Iid as bIid, TotalAmount FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid<=10 GROUP BY Iid) as a WHERE a.TotalAmount = (SELECT MAX(TotalAmount) FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid<=10 GROUP BY Iid) as b)) c ON Ingredients.Iid=c.bIid;";
            SqlConnection connection = connectToSQL();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            var bestSellerIid = 0; var bestSellerName = ""; var bestSellerAmount = 0;
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                bestSellerIid = reader.GetInt32(0);
                bestSellerName = reader.GetString(1);
                bestSellerAmount = reader.GetInt32(2);
            }
            reader.Close();
            ans += "Best Seller Ball Taste:\n Iid:"+bestSellerIid+", "+bestSellerName+", "+bestSellerAmount+"\n";
            sql = "SELECT Iid, Name, TotalAmount FROM Ingredients INNER JOIN (SELECT Iid as bIid, TotalAmount FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid>=11 AND Iid<=13 GROUP BY Iid) as a WHERE a.TotalAmount = (SELECT MAX(TotalAmount) FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid>=11 AND Iid<=13 GROUP BY Iid) as b)) c ON Ingredients.Iid=c.bIid;";
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                bestSellerIid = reader.GetInt32(0);
                bestSellerName = reader.GetString(1);
                bestSellerAmount = reader.GetInt32(2);
            }
            ans += "Best Seller Extra Taste:\n Iid:"+bestSellerIid+", "+bestSellerName+", "+bestSellerAmount+"\n";
            reader.Close();
            sql = "SELECT Iid, Name, TotalAmount FROM Ingredients INNER JOIN (SELECT Iid as bIid, TotalAmount FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid>=14 AND Iid<=16 GROUP BY Iid) as a WHERE a.TotalAmount = (SELECT MAX(TotalAmount) FROM (SELECT Sum(amount) as TotalAmount, Iid as Iid FROM Portions WHERE Iid>=14 AND Iid<=16 GROUP BY Iid) as b)) c ON Ingredients.Iid=c.bIid;";
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read()) // READ THE SQL ANSWER HERE
            { 
                bestSellerIid = reader.GetInt32(0);
                bestSellerName = reader.GetString(1);
                bestSellerAmount = reader.GetInt32(2);
            }
            ans += "Best Seller Cup Type:\n Iid:"+bestSellerIid+", "+bestSellerName+", "+bestSellerAmount+"\n";
            reader.Close();
            return ans;
        }


    }
}