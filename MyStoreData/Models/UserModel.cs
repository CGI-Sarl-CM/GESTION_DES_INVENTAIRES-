using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyStoreData.Models
{
    public partial class UserModel:RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; }= ObjectId.GenerateNewId();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string UserName { get; set; }
        public required string Password { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    }
}
