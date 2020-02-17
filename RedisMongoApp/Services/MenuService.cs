using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RedisMongoApp.Interfaces;
using RedisMongoApp.Models;

namespace RedisMongoApp.Services
{
    public class MenuService : IOperations
    {
        private readonly IMongoDatabase database;
        private IMongoCollection<Menu> collection;

        public MenuService(IMenuStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<Menu>("Menus");

        }

        public void add(Menu menu)
        {
            collection.InsertOne(menu);
        }

        public Menu get(string id)
        {
            collection = database.GetCollection<Menu>("Menus");
            var readFilter = Builders<Menu>.Filter.Eq("id", id);
            var menu = collection.Find(readFilter).FirstOrDefault();

            return menu;
        }

        public IEnumerable getAll()
        {
            var menus = collection.Find(new BsonDocument());
            return menus.ToEnumerable();
        }
    }
}
