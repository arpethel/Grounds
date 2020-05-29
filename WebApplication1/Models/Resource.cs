using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class Resource
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
}
