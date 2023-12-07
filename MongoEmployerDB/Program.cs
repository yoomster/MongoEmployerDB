using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;



namespace MongoEmployerDB
{
    class Program
    {
        private static MongoDBDataAccess db;
        private static readonly string tableName = "EmployersInfo";

        static void Main(string[] args)
        {
            db = new MongoDBDataAccess("MongoEmployerDB", GetConnectionString());

            PersonModel person = new PersonModel
            {
                FirstName = "Naomi",
                LastName = "Perenboom"
            };

            person.Addresses.Add(new AddressModel {AddressName="Frits", HouseNumber="104", Zipcode="5616TZ", City="Ehv", Country="NL"});
            person.Addresses.Add(new AddressModel { AddressName = "Hop", HouseNumber = "3", Zipcode = "5616NG", City = "Ehv", Country = "NL" });


            person.Employers.Add(new EmployerModel {CompanyName ="Rabobank", JobTitle ="Adviseur" });
            person.Employers.Add(new EmployerModel { CompanyName = "Van Lanschot", JobTitle = "Adviseur" });

            CreatePerson(person);

            GetAllPeople();

            //AA: 00000000-0000-0000-0000-000000000000
            //NP: 639ee377-4d10-4dc4-bc4b-3ad1f6ebcb97


            Console.WriteLine("MongoDB procesed");
            Console.ReadLine();
        }

        private static void GetAllPeople()
        {
            var people = db.LoadAllRecords<PersonModel>(tableName);

            foreach (var person in people)
            {
                Console.WriteLine($"{person.Id}:{person.FirstName} {person.LastName}");
            }
        }

        private static void CreatePerson(PersonModel person)
        {
            db.UpsertRecord(tableName, person.Id, person);
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