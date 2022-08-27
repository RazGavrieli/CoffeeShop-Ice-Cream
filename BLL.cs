using DAL;
using DataProtocol; // dataprotocol is written by Amit
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

        public int createSale(Sale newsale) {
            sqlAdapater DAL = new sqlAdapater();
            var result = DAL.createSale(newsale);
            return 1;
        }
    }
}