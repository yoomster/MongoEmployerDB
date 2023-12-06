using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class MongoDBDataAccess
    {
        private IMongoDatabase db;
        public MongoDBDataAccess(string dbName, string connectionString)
        {
            //connect to local machine
            var client = new MongoClient(connectionString);

            //connect to specified db, on specified MongoDB server
            db = client.GetDatabase(dbName);
        }
    }
}
