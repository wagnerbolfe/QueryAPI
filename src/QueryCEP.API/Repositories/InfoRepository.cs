using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using QueryCEP.API.Entities;

namespace QueryCEP.API.Repositories
{
    public class InfoRepository : IInfoRepository
    {
        private readonly IMongoCollection<Cep> dbCollection;
        private readonly FilterDefinitionBuilder<Cep> filterBuilder = Builders<Cep>.Filter;

        public InfoRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("InfoDb");
            dbCollection = database.GetCollection<Cep>("Ceps");
        }

        public async Task<IReadOnlyCollection<Cep>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Cep> GetAsync(string cep_number)
        {
            FilterDefinition<Cep> filter = filterBuilder.Eq(entity => entity.CepNumber, cep_number);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Cep entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Cep entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            FilterDefinition<Cep> filter = filterBuilder.Eq(existingEntity => existingEntity.CepNumber, entity.CepNumber);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(string cep_number)
        {
            FilterDefinition<Cep> filter = filterBuilder.Eq(entity => entity.CepNumber, cep_number);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}