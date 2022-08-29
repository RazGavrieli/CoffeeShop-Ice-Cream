using DAL;
using DataProtocol; // dataprotocol is written by Amit

namespace BLL
{
    public class businessLogic
    {
        public Sale newSale() {
            Sale newsale = new Sale();

            Console.WriteLine("New sale recorded at "+newsale.date);

            sqlAdapater DAL = new sqlAdapater();
            DAL.editSale(newsale);
            Console.WriteLine("Its in the SQL with id: "+newsale.Id);
            return newsale;
        }
        public int sumSale(Sale newsale) {
            sqlAdapater DAL = new sqlAdapater();
            DAL.editSale(newsale);
            newsale.boolClosedSale = true;
            return 1;
        }

        public void finishSale(Sale currsale) {
            /*
            This function updates the final sum of the sale and uploads all the Portions used to the SQL server.
            */
            sqlAdapater DAL = new sqlAdapater();



            // maybe call for update 
            DAL.addIngredientToSale(currsale, 3, 4); // << Example of adding Iid = 3 and amount = 4 (4 balls of something with id 3)
        }

        public Boolean initializeDatabase() {
            sqlAdapater DAL = new sqlAdapater();
            DAL.createDatabase();
            DAL.createTables();
            return true;
        }
    }
}