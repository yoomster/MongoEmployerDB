﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
        public List<EmployerModel> Employers { get; set; } = new List<EmployerModel>();
    }
}