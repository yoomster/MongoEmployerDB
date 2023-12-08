using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
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


            person.Employers.Add(new EmployerModel {Employer ="Rabobank", JobTitle ="Adviseur" });
            person.Employers.Add(new EmployerModel { Employer = "Van Lanschot", JobTitle = "Adviseur" });

            //CreatePerson(person);
            //GetAllPeople();


            //GetPersonById("639ee377-4d10-4dc4-bc4b-3ad1f6ebcb97");
            //AA: 00000000-0000-0000-0000-000000000000
            //NP: 639ee377-4d10-4dc4-bc4b-3ad1f6ebcb97

            //UpdatePersonName("639ee377-4d10-4dc4-bc4b-3ad1f6ebcb97", "LASTNAME", "Peer");

            RemoveCompanyFromUser("639ee377-4d10-4dc4-bc4b-3ad1f6ebcb97", "Rabobank");

            Console.WriteLine("MongoDB procesed");
            Console.ReadLine();
        }


        private static void RemoveCompanyFromUser(string id, string employer)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            person.Employers = person.Employers.Where(x => x.Employer != employer).ToList();

            db.UpsertRecord(tableName, person.Id, person);
        }

        private static void UpdatePersonName(string id, string changingProperty, string updatedInfo)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            if (changingProperty.ToLower() == "firstname")
            {
                person.FirstName = updatedInfo;
            }
            else if (changingProperty.ToLower() == "lastname")
            {
                person.LastName = updatedInfo;
            }

            db.UpsertRecord(tableName, person.Id, person);

        }


        private static void GetPersonById(string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            Console.WriteLine($"{person.Id}:{person.FirstName} {person.LastName}");
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