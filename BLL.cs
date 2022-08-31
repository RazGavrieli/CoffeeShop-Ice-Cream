using DAL;
using DataProtocol; 

namespace BLL
{
    public class businessLogic
    {
        public Boolean noSQL { get; set; }
        sqlAdapater DAL = new sqlAdapater();
        noSQLadapter nDAL = new noSQLadapter();

        public Sale newSale() {
            if (noSQL) {
                Sale newsale = new Sale();
                nDAL.editSale(newsale);
            return newsale;
            } else {
                Sale newsale = new Sale();
                DAL.editSale(newsale);
                return newsale;
            }
        }
        public int sumSale(Sale currsale) {
            if (!currsale.CheckForValidOrder()) {
                throw new ArgumentException("Can't sum up an invalid order"); 
            }
            if (noSQL) {
                currsale.boolClosedSale = true;
                nDAL.editSale(currsale);
                Console.WriteLine(currsale.TotalPrice);
            } else {
                currsale.boolClosedSale = true;
                DAL.editSale(currsale); 
                DAL.recordPortionsOfSale(currsale);
            }
            return 1;
        }

        public Boolean initializeDatabase() {
            if (noSQL) { }
            else {
            DAL.createDatabase();
            DAL.createTables();
            DAL.loadIngredientsTable();
            }
            return true;
        }
        
        public string getReceipt(int Sid) {
            string ans = "";
            if (noSQL) {
                ans = nDAL.getReceipt(Sid);
            }
            else {
                ans = DAL.getReceipt(Sid); 
            }
            return ans; 
        }
        public string getDaySum(string askedDate) {
            if (askedDate[2]!='/'||askedDate[5]!='/'||askedDate.Length!=10) {
                throw new ArgumentException("Date is not in the correct format: XX/XX/XXXX"); 
            }
            string ans = "";
            if (noSQL) {
            ans = nDAL.getDaySum(askedDate);
             }
            else {
            ans = DAL.getDaySum(askedDate);
            }
            return ans;
        } 

        public string unfinshedSales(Boolean delete) {
            string ans = "";
            if (noSQL) { 
                ans = nDAL.unfinishedSales();
            }
            else {
                ans = DAL.unfinishedSales();
            if (delete)
                DAL.deleteUnfinishedSales();
            }
            return ans;
        }

        public string getBestSellers() {
            string ans = "";
            if (noSQL) {
                ans = nDAL.getBestSellers();
             }
            else {
            ans = DAL.getBestSellers();
            }
            return ans;
        }
    }
}