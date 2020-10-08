using KnowledgeApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeApi.Services
{
        public class BaseMongoRepository<TModel>
        where TModel : MongoBaseModel
        {
            private readonly IMongoCollection<TModel> mongoCollection;

            public BaseMongoRepository(string mongoDBConnectionString, string dbName, string collectionName)
            {
                var client = new MongoClient(mongoDBConnectionString);
                var database = client.GetDatabase(dbName);
                mongoCollection = database.GetCollection<TModel>(collectionName);
            }

            public virtual async Task<List<TModel>> GetList()
            {
                return await mongoCollection.Find(book => true).ToListAsync();
            }
        

            public virtual async Task<TModel> GetById(string id)
            {
                //var docId = new ObjectId(id);
                return await mongoCollection.Find<TModel>(m => m.Id == id).FirstOrDefaultAsync();
            }

            public virtual async Task<TModel> Create(TModel model)
            {
                await mongoCollection.InsertOneAsync(model);
                return model;
            }
            public virtual async Task<TModel> CreateNested(TModel model, TModel NModel)
            {
                await mongoCollection.InsertOneAsync(model);
            //string Id = mongoCollection.FindAsync(x => x.Id == model.Id).Id.ToString();
                await mongoCollection.ReplaceOneAsync(m => m.Id == model.Id, NModel);
                
                
                return model;
            }

            public virtual async Task Update( TModel model)
            {
                //var docId = new ObjectId(id);
                await mongoCollection.ReplaceOneAsync(m => m.Id == model.Id, model);
            }

            public virtual async Task Delete(TModel model)
            {
               await mongoCollection.DeleteOneAsync(m => m.Id == model.Id);
            }

            public virtual async Task Delete(string id)
            {
                //var docId = new ObjectId(id);
                await mongoCollection.DeleteOneAsync(m => m.Id == id);
            }
        }

    }

