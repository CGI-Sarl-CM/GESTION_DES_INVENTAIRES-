global using MongoDB.Bson;
global using Realms;
using CGIERPData.Enums;
using CGIERPData.Models.EmbeddedObjects;
using CGIERPData.Models.QHSEModels;

namespace CGIERPData.Models;

public partial class ItemModel : RealmObject
{

    [PrimaryKey]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public required string Name { get; set; }
    public string SerialNumber { get; set; }
    public int StockQuantity { get; set; } = 1;

    public required CategoryModel Category { get; set; }
    public required DepartementModel Department { get; set; }

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    // Backlink: Get all QHSE Assignments linked to this specific item automatically
    [Backlink(nameof(QhseAssignmentModel.Item))]
    public IQueryable<QhseAssignmentModel> AssignmentsHistory { get; }
}
