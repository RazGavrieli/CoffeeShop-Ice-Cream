using MongoDB.Bson;
using MongoDB.Driver;
using DataProtocol;
using System.Linq;

namespace DAL {
    class noSQLadapter {
        /*
        This class is used to communicate with a MongoDB cloud (or local server). (Data-Access-Layer)
        Inorder to connect to your own server, change the dbClient MongoClient connection string in the function 'connectToNOSQL()'.
        */

    public IMongoDatabase connectToNOSQL()
    {
    /* This function returns the connection to the server. It is used in almost every function in this class. 
    */
        MongoClient dbClient = new MongoClient("mongodb+srv://Raz:2580@cluster0.6clvl3i.mongodb.net/test");
        IMongoDatabase database = dbClient.GetDatabase ("IceCreamCoffeeShop");
        return database;
    }

    public void loadIngredientsTable() {
        /*
        NOTICE: THE NAME OF THE FUNCTION INCLUDES THE WORD 'TABLE', EVEN THOUGH IT CREATES A DOCUMENT AND NOT A TABLE. (noSQL DAL). 
            This function creates the lookup document for the Ingredients. 
            The database manager should call this function everytime the IceCream shop updates the tastes. 
        */
        IMongoDatabase datebase = connectToNOSQL();
        var IngredientsCollection = datebase.GetCollection<BsonDocument>("Ingredient-LOOKUP");
        var values = Enum.GetValues(typeof(Taste));
        int i = 1;
        foreach (var ingredientName in values) {
            string json = "{'name': '"+ingredientName+"', 'Iid': "+i+"}";
            IngredientsCollection.InsertOne(BsonDocument.Parse(json));
            i++;
        }
        values = Enum.GetValues(typeof(ExtraTaste));
        foreach (var extraName in values) {
            string json = "{'name': '"+extraName+"', 'Iid': "+i+"}";
            IngredientsCollection.InsertOne(BsonDocument.Parse(json));
            i++;
        }

        values = Enum.GetValues(typeof(CupType));
        foreach (var cupName in values) {
            string json = "{'name': '"+cupName+"', 'Iid': "+i+"}";
            IngredientsCollection.InsertOne(BsonDocument.Parse(json));
            i++;
        }
    }

    public Boolean editSale(Sale newsale) {
            /*
                Despite it's misleading name, this function is used to add a new sale to the database or to update an exisiting sale. 
                if the Sid of @variable 'newsale' is 0, it means that the sale is still not stored in the database. 
                Else, the sale is already stored in the database so the function will Update the sale's information in the database. 
            */
        if (newsale.Sid == 0) {
            newsale.Sid=1;
            IMongoDatabase datebase = connectToNOSQL();
            var salesCollection = datebase.GetCollection<Sale> ("sales");
            var filter = Builders<Sale>.Filter.Gt("Sid", 0); // any
            newsale.Sid = ((int)salesCollection.Count(filter)) + 1;
            salesCollection.InsertOne(newsale);
        } else {
            IMongoDatabase datebase = connectToNOSQL();
            var salesCollection = datebase.GetCollection<Sale> ("sales");
            var filter = Builders<Sale>.Filter.Eq("_id", newsale.Id);
            var update = Builders<Sale>.Update.Set("CupType", newsale.CupType).Set("Balls", newsale.Balls).Set("ExtrasOnBalls", newsale.ExtrasOnBalls).Set("boolClosedSale",newsale.boolClosedSale).Set("TotalPrice", newsale.TotalPrice);
            salesCollection.UpdateOne(filter, update); 

        }
        return true;
    }

    public string getReceipt(int Sid) {
            /*
                Uses an Sid to find the sale in the database and creates a receipt for it. 
            */
            string ans = ""; 
            IMongoDatabase datebase = connectToNOSQL();
            var salesCollection = datebase.GetCollection<Sale> ("sales");
            var filter = Builders<Sale>.Filter.Eq("Sid", Sid);
            var sale = salesCollection.Find(filter).ToList()[0];
            ans += "Recipt for "+Sid+", "+sale.date+", TOTAL: "+sale.TotalPrice+"$\n";
            ans += "\tIngredient List\n";
            var balls = sale.Balls;
            var extras = sale.ExtrasOnBalls;
            foreach (var b in balls)
                ans += "\t"+b.Taste+"\n";
            foreach (var e in extras)
                ans += "\t"+e.ExtraTaste+"\n";

            return ans;
        }
    public string unfinishedSales() {
        /*
            Returns all the unfinished sales in the database. 
            A sale is 'unfinished' if in the document 'sales', It's 'boolClosedSale' field is false. 
        */
        string ans = "UNFINISHED SALES:\n";
        IMongoDatabase datebase = connectToNOSQL();
        var salesCollection = datebase.GetCollection<Sale> ("sales");
        var filter = Builders<Sale>.Filter.Eq("boolClosedSale", false);
        var saleList = salesCollection.Find(filter).ToList();
        foreach (var sale in saleList)
            ans += "Sid: "+sale.Sid+"\tDate: "+sale.date+"\n";

        return ans;
    }
    public string getDaySum(string askedDate) {
        /*
            Finds in the collection sales all the sales that were created on the asked date and returns a summary. 
        */
        string ans = "";
        var amount = 0; var sum = 0; var avg = 0;
        IMongoDatabase datebase = connectToNOSQL();
        var salesCollection = datebase.GetCollection<Sale> ("sales");
        var filter = Builders<Sale>.Filter.Regex("date", new BsonRegularExpression($".*{askedDate}.*")); 
        amount = ((int)salesCollection.Count(filter));

        var saleList = salesCollection.Find(filter).ToList();
        foreach (var sale in saleList)
            sum += (int)sale.TotalPrice;

        if (amount>0)
            avg = sum/amount;
        ans = "amount of sales: "+amount+"\nsum of sales: "+sum+"\n avarage: "+avg;
        return ans;
    }
    public string getBestSellers() {
        /*
            Counts the ingredients through all the sales in the 'sales' document and returns the best sellers of all time. 
        */
        string ans = "";
        IMongoDatabase datebase = connectToNOSQL();
        var salesCollection = datebase.GetCollection<Sale> ("sales");
        var filter = Builders<Sale>.Filter.Gt("Sid", 0); // any
        var saleList = salesCollection.Find(filter).ToList();
        int[] balls = new int[10];
        int[] extras = new int[3];
        int[] cups = new int[3];
        
        foreach(var sale in saleList) {
            foreach (var ball in sale.Balls) {
                balls[((int)ball.Taste)]++;
            }
            foreach (var extra in sale.ExtrasOnBalls) {
                extras[((int)extra.ExtraTaste)]++;
            }
            cups[((int)sale.CupType)]++;
        }

        int maxAmountBall = balls.Max();
        int maxAmountExtra = extras.Max();
        int maxAmountCup = cups.Max();

        int maxtBall = balls.ToList().IndexOf(maxAmountBall);
        int maxExtra = extras.ToList().IndexOf(maxAmountExtra)+10;
        int maxCup = cups.ToList().IndexOf(maxAmountCup)+13;

        var IngredientsLookup = datebase.GetCollection<BsonDocument> ("Ingredient-LOOKUP");
        var ballFilter = Builders<BsonDocument>.Filter.Eq("Iid", maxtBall);
        var extraFilter = Builders<BsonDocument>.Filter.Eq("Iid", maxExtra);
        var cupFilter = Builders<BsonDocument>.Filter.Eq("Iid", maxCup);
        var MaxBallName = IngredientsLookup.Find(ballFilter).ToList()[0].GetElement("name");
        var MaxExtraName = IngredientsLookup.Find(extraFilter).ToList()[0].GetElement("name");
        var MaxCupName = IngredientsLookup.Find(cupFilter).ToList()[0].GetElement("name");

        ans += "Best Seller Ball Taste:\n Iid="+maxtBall+", "+MaxBallName+", "+maxAmountBall+"\n";
        ans += "Best Seller Extra Taste:\n Iid="+maxExtra+", "+MaxExtraName+", "+maxAmountExtra+"\n";
        ans += "Best Seller Cup:\n Iid="+maxCup+", "+MaxCupName+", "+maxAmountCup+"\n";


        return ans;
    }

    
    }
}