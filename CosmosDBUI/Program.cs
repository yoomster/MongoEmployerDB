using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace CosmosDBUI
{
    class Program
    {
        static async Task Main(string[] args)
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

        private static (string endpointUrl, string primaryKey, string databaseName, string containerName) GetCosmosInfo()
        {
            (string endpointUrl, string primaryKey, string databaseName, string containerName) output;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output.endpointUrl = config.GetValue<string>("CosmosDB:EndpointUrl");
            output.primaryKey = config.GetValue<string>("CosmosDB:PrimaryKey");
            output.databaseName = config.GetValue<string>("CosmosDB:DatabaseName");
            output.containerName = config.GetValue<string>("CosmosDB:ContainerName");

            return output;
        }
    }
}