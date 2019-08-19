using AuthService.Core.IService;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.MongoDB
{
    public class MongoDbHelper
    {
        MongoClient client;
        IMongoDatabase logDb;
        public MongoDbHelper(IConfiguration configuration)
        {
            var c = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            logDb = c.GetDatabase("log");
            this.client = c;
        }

        public async Task<List<T>> FindListNoPagdeAsync<T>(string collectionName, Expression<Func<T, bool>> expression)
        {
            var collection= logDb.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Where(expression);
            var result=await collection.FindAsync(filter);
            return await result.ToListAsync();
        }
        public async Task<List<T>> FindPagdeListAsync<T>(string collectionName,Expression<Func<T, bool>> expression, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            var collection = logDb.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Where(expression);
            FindOptions<T, T> options = new FindOptions<T, T>();
            options.Limit = pageSize;
            options.Skip = pageSize * (pageIndex - 1);
            options.Sort = sort;
            var result = await collection.FindAsync(filter,options);

            return await result.ToListAsync();
        }

        public async Task<UpdateResult> UpadateOneAsync<TDocument, TField>(string collectionName, Expression<Func<TDocument, bool>> filter, FieldDefinition<TDocument, TField> field, TField value)
        {
            var collection = logDb.GetCollection<TDocument>(collectionName);
            var update= Builders<TDocument>.Update.Set(field, value);
            var result= await collection.UpdateOneAsync(filter, update);
            return result;
        }
        public async Task<UpdateResult> UpadateManyAsync<TDocument, TField>(string collectionName, Expression<Func<TDocument, bool>> filter, FieldDefinition<TDocument, TField> field, TField value)
        {
            var collection = logDb.GetCollection<TDocument>(collectionName);
            var update = Builders<TDocument>.Update.Set(field, value);
            var result = await collection.UpdateManyAsync(filter, update);
            return result;
        }
        public async Task<DeleteResult> DeleteOneAsync<TDocument, TField>(string collectionName, Expression<Func<TDocument, bool>> filter)
        {
            var collection = logDb.GetCollection<TDocument>(collectionName);
            var result= await collection.DeleteOneAsync(filter);
            return result;
        }
        public async Task<DeleteResult> DeleteManyAsync<TDocument, TField>(string collectionName, Expression<Func<TDocument, bool>> filter)
        {
            var collection = logDb.GetCollection<TDocument>(collectionName);
            var result = await collection.DeleteManyAsync(filter);
            return result;
        }
    }
}
