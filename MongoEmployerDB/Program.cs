using DataAccessLibrary;
using Microsoft.Extensions.Configuration;



namespace MongoEmployerDB
{
    class Program
    {
        private static MongoDBDataAccess db;
        static void Main(string[] args)
        {
            db = new MongoDBDataAccess("",GetConnectionString());
        }
        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}