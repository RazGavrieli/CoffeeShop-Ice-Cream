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

                builder.DataSource = @"(localdb)\MSSQLLocalDB"; // INSERT HERE CORRECT SETTINGS
                builder.InitialCatalog = "ArielDB";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
        public Boolean createSale(Sale newsale)
        {
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
                                var name = reader.GetString(1);
                                var age = reader.GetInt32(2);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
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
                                ResultSale.totalPrice = reader.GetFloat(3);
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