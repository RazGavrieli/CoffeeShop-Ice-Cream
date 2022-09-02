using DAL;
using DataProtocol; 

namespace BLL
{
    public class businessLogic
    {
        /*
        This class, together with the classes under 'DataProtocol' namespace, represent the Business-Logic-Layer of the program. 
        It includes all the technical conditions and the possible functions that the IceCreamStore should implement. 

        This class has the ability to connect either with an SQL DAL or an noSQL DAL. It has a Boolean 'noSQL' that selects which DAL will be used. 
        */
        public Boolean noSQL { get; set; }
        sqlAdapater DAL = new sqlAdapater();
        noSQLadapter nDAL = new noSQLadapter();

        public Sale newSale() {
            /* This function creates a new sale (from scratch) and sends it to the DAL intefeace to be stored in the database. 
            */
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
            /* This function receives a Sale, and sends it to the DAL interface to be >UPDATED<. 
            If the sale is not already in the database, it will cause an exception.  
            */
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
            /* This function is used to order the DAL to reinitialize the database.
            Keep in mind that if an SQL server is used, the tables will get deleted. 
            */
            if (noSQL) {
                nDAL.loadIngredientsTable();
            } else {
                DAL.createDatabase();
                DAL.createTables();
                DAL.loadIngredientsTable();
            }
            return true;
        }
        
        public string getReceipt(int Sid) {
            /* This function receives an Sid (sale ID), and sends it to the DAL interface to get a recepit from the database. 
            */
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
            /* This function gets a date (as a string XX/XX/XXXX) and sends it to the DAL interface to get a summary of this day from the database. 
            */
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
            /* This function returns all the unfinished sales that are in the database.  (Through the DAL interface of course)
            If the Boolean 'delete' is true, it will also delete the unfinished sales.
            */
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
            /* This function uses the DAL interface to get the best sellers in the database. 
            */
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