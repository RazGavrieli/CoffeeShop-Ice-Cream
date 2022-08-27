using DataProtocol;
using System.Data.SqlClient;

namespace DAL
{
    public class sqlAdapater
    {
        // public List<Sale> GetSales(int MinAge)
        // {
        //     // var Result = new List<Person>();
        //     try
        //     {
        //         SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        //         builder.DataSource = @"(localdb)\MSSQLLocalDB"; // INSERT HERE CORRECT SETTINGS
        //         builder.InitialCatalog = "ArielDB";             // INSERT HERE CORRECT SETTINGS

        //         using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //         {
        //             connection.Open();

        //             // String sql = $"SELECT * FROM dbo.People WHERE Age > {MinAge}"; // INSERT SQL QUERRY HERE

        //             using (SqlCommand command = new SqlCommand(sql, connection))
        //             {
        //                 using (SqlDataReader reader = command.ExecuteReader())
        //                 {
        //                     // while (reader.Read()) // READ THE SQL ANSWER HERE
        //                     // {
        //                     //     var name = reader.GetString(1);
        //                     //     var age = reader.GetInt32(2);
        //                     //     Result.Add(new Person {Name = name,Age = age});
        //                     // }
        //                 }
        //             }
        //         }
        //     }
        //     catch (SqlException e)
        //     {
        //         Console.WriteLine(e.ToString());
        //     }
        //     return Result;
        // }

        

    }
}