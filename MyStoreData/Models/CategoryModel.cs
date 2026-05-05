using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyStoreData.Models
{
    public partial class CategoryModel : RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; }=  ObjectId.GenerateNewId();
        public required string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public IList<ItemModel> Items { get; }
    }
}
