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
        public int sumSale(Sale currsale) {
            if (!currsale.CheckForValidOrder()) {
                throw new ArgumentException("Can't sum up an invalid order"); 
            }
            sqlAdapater DAL = new sqlAdapater();
            DAL.editSale(currsale); 
            currsale.boolClosedSale = true;
            DAL.recordPortionsOfSale(currsale);
            return 1;
        }

        public Boolean initializeDatabase() {
            sqlAdapater DAL = new sqlAdapater();
            DAL.createDatabase();
            DAL.createTables();
            DAL.loadIngredientsTable();
            return true;
        }
        
        public string getRecipt(int Sid) {
            sqlAdapater DAL = new sqlAdapater();
            string ans = DAL.getRecipt(Sid);
            return ans; 
        }
    }
}