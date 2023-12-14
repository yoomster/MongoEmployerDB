using DataAccessLibrary.Models;
using Microsoft.Azure.Cosmos;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class CosmosDBDataAccess
    {
        private readonly string _endpointUrl;
        private readonly string _primaryKey;
        private readonly string _databaseName;
        private readonly string _containerName;
        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public CosmosDBDataAccess(string endpointUrl,
                                  string primaryKey,
                                  string databaseName,
                                  string containerName)
        {
            _endpointUrl = endpointUrl;
            _primaryKey = primaryKey;
            _databaseName = databaseName;
            _containerName = containerName;

            _cosmosClient = new CosmosClient(_endpointUrl, _primaryKey);
            _database = _cosmosClient.GetDatabase(_databaseName);
            _container = _database.GetContainer(_containerName);
        }

        public async Task<List<T>> LoadAllRecordsAsync<T>()
        {
            
        }
        public async Task<T> LoadRecordByIdAsync<T>(string id)
        {
            
        }

        public async Task UpsertRecordAsync<T>(T record)
        {

        }

        public async Task DeleteRecordAsync<T>(string id)
        {

        }
    }
}
