using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace CosmosDBUI
{
    class Program
    {
        private static CosmosDBDataAccess db;

        static async Task Main(string[] args)
        {
            var c = GetCosmosInfo();
            db = new CosmosDBDataAccess(c.endpointUrl, c.primaryKey, c.databaseName, c.containerName);

            PersonModel user = new PersonModel
            {
                FirstName = "Naomi",
                LastName = "Perenboom"
            };
            user.Addresses.Add(new AddressModel { AddressName = "Frits Philipslaan", HouseNumber = "104", Zipcode="5616TZ", City ="Ehv", Country ="NL"});
            user.Addresses.Add(new AddressModel { AddressName = "Hopmanstr", HouseNumber ="3", Zipcode ="5703HG", City ="Helm", Country = "NL" });

            user.Employers.Add(new EmployerModel { Employer = "Rabobank", JobTitle = "Adviseur"});
            user.Employers.Add(new EmployerModel { Employer = "Van Lanschot", JobTitle = "Adviseur"});



            PersonModel user2 = new PersonModel
            {
                FirstName = "Adam",
                LastName = "Akil"
            };
            user2.Addresses.Add(new AddressModel { AddressName = "Frits Philipslaan", HouseNumber = "104", Zipcode = "5616TZ", City = "Ehv", Country = "NL" });
            user2.Addresses.Add(new AddressModel { AddressName = "Frederik", HouseNumber = "189-67", Zipcode ="5616NG", City ="Ehv", Country = "NL" });

            user2.Employers.Add(new EmployerModel { Employer = "IT & Care", JobTitle = "Developer" });
            user2.Employers.Add(new EmployerModel { Employer = "Heijmans", JobTitle = "Developer" });

            //await CreatePerson(user);
            //await CreatePerson(user2);

            //await GetAllPeople();

            //223a9045-9fbf-474b-9585-cbb0a526c76e: Naomi Perenboom
            //22d6fa08-e028-409d-a773-e0667a671247: Adam Akil


            Console.WriteLine("CosmosDB procesed");
            Console.ReadLine();
        }


        private static async Task RemovePerson(string id, string lastName)
        {
            await db.DeleteRecordAsync<PersonModel>(id, lastName);
        }

        private static async Task RemoveEmployer (string id, string employer)
        {
            var person = await db.LoadRecordByIdAsync<PersonModel>(id);

            person.Employers = person.Employers.Where (x => x.Employer != employer).ToList();

            db.UpsertRecordAsync(person);
        }


        private static async Task GetPersonById(string id)
        {
            var person = await db.LoadRecordByIdAsync<PersonModel>(id);

            Console.WriteLine($"{person.Id}: {person.FirstName} {person.LastName}");
        }

        private static async Task GetAllPeople()
        {
            var people = await db.LoadAllRecordsAsync<PersonModel>();

            foreach (var person in people)
            {
                Console.WriteLine($"{person.Id}: {person.FirstName} {person.LastName}");
            }
        }

        private static async Task CreatePerson(PersonModel person)
        {
            await db.UpsertRecordAsync(person);
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