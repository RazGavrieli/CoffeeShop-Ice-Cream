using DAL;
using DataProtocol; 

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
        
        public string getReceipt(int Sid) {
            sqlAdapater DAL = new sqlAdapater();
            string ans = DAL.getReceipt(Sid);
            return ans; 
        }
        public string getDaySum(string askedDate) {
            if (askedDate[2]!='/'||askedDate[5]!='/'||askedDate.Length!=10) {
                throw new ArgumentException("Date is not in the correct format: XX/XX/XXXX"); 
            }
            sqlAdapater DAL = new sqlAdapater();
            string ans = DAL.getDaySum(askedDate);
            return ans;
        } 

        public string unfinshedSales() {
            sqlAdapater DAL = new sqlAdapater();
            string ans = DAL.unfinishedSales();
            // DAL.deleteUnfinishedSales(); 
            return ans;
        }

        public string getBestSellers() {
            sqlAdapater DAL = new sqlAdapater();
            string ans = DAL.getBestSellers();
            return ans;
        }
    }
}