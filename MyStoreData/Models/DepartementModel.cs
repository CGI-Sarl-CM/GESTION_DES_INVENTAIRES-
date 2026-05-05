using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyStoreData.Models
{
    public partial class DepartementModel : RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public required string Name { get; set; }
        public   IList<CategoryModel> CategoryList { get;}
        public IList<ItemModel> ItemList { get; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    }
}
