using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
/*          MongoCRUD db = new MongoCRUD("Resources");
            db.InsertRecord("Resources", new Resource { ResourceTitle = "Stack Overflow", ResourceDescription = "This is stack overflow test", Uri = "www.stackoverflow.com", UpVote = 10, DownVote = 10 });
            var resources = db.LoadResources<Resource>("Resources");*/
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

/*    public class MongoCRUD
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

        *//*public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                
                );
        }*//*
    }*/
}
