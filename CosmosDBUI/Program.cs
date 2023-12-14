using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace CosmosDBUI
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("CosmosDB procesed");
            Console.ReadLine();
        }


        private static void RemovePerson(string id)
        {
        
        }

        private static void RemoveCompanyFromUser(string id, string employer)
        {
 
        }

        private static void UpdatePersonName(string id, string changingProperty, string updatedInfo)
        {

        }


        private static void GetPersonById(string id)
        {

        }

        private static void GetAllPeople()
        {

        }

        private static void CreatePerson(PersonModel person)
        {
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