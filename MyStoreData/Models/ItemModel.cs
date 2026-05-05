global using Realms;
global using MongoDB.Bson;

namespace MyStoreData.Models
{
    public partial class ItemModel : RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.Empty;
        public required string Name { get; set; }
        public  required  CategoryModel Category { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    }
}