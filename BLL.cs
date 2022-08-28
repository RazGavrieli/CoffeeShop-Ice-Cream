using DAL;
using DataProtocol; // dataprotocol is written by Amit

using System.Globalization;
namespace BLL
{
    public class businessLogic
    {
        // public List<Person> GetPeople(int mage)
        // {
        //     sqlAdapter DAL=new sqlAdapter();
        //     var Result=DAL.GetPeople(mage);
        //     return Result;
        // }
        public void newSale() {
            Sale newsale = new Sale();
            DateTime localDate = DateTime.Now;
            newsale.date = localDate.ToString();
            Console.WriteLine("New sale recorded at "+newsale.date);

            sqlAdapater DAL = new sqlAdapater();
            DAL.editSale(newsale);
            Console.WriteLine("Its in the SQL with id: "+newsale.Id);
        }
        public int createSale(Sale newsale) {
            sqlAdapater DAL = new sqlAdapater();
            //var result = DAL.createSale(newsale);
            return 1;
        }

        public Boolean initializeDatabase() {
            sqlAdapater DAL = new sqlAdapater();
            DAL.createDatabase();
            DAL.createTables();
            return true;
        }
    }
}