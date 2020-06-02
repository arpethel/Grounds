using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using MongoDB.Driver;

namespace WebApplication1.Services
{
    public class ResourceService
    {
        private readonly IMongoCollection<Resource> _resources;

        public ResourceService(IResourceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _resources = database.GetCollection<Resource>(settings.ResourceCollectionName);

            //var resource = new Resource { ResourceTitle = "Test Overflow", ResourceDescription = "This is Create test", Uri = "www.testoverflow.com", UpVote = 11, DownVote = 10 };
           // _resources.InsertOne(resource);
        }

        public List<Resource> Get() =>
            _resources.Find(resource => true).ToList();

        public Resource Get(string id) =>
            _resources.Find<Resource>(resource => resource.Id == Guid.Parse(id)).FirstOrDefault();

        public Resource Create(Resource resource)
        {
            _resources.InsertOne(resource);
            return resource;
        }

        public void Update(string id, Resource resourceIn) =>
            _resources.ReplaceOne(resource => resource.Id.ToString() == id, resourceIn);

        public void Remove(Resource resourceIn) =>
            _resources.DeleteOne(resource => resource.Id == resource.Id);

        public void Remove(string id) =>
            _resources.DeleteOne(resource => resource.Id.ToString() == id);
    }
}
