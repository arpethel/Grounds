using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("Resources");
           db.InsertRecord("Resources", new ResourceModel { ResourceTitle = "Stack Overflow", ResourceDescription = "This is stack overflow", Uri = "www.stackoverflow.com", UpVote = 10, DownVote = 10 });
            var resources = db.LoadResources<ResourceModel>("Resource");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class ResourceModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string ResourceTitle { get; set; }
        public string ResourceDescription { get; set; }
        public string Uri { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }

        public void HandleUpVote()
        {
            UpVote++;
        }
        public void HandleDownVote()
        {
            DownVote--;
        }

    }

    public class UserModel
    {   [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T resource)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(resource);
        }

        public List<T> LoadResources<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        /*public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                
                );
        }*/
    }
}
