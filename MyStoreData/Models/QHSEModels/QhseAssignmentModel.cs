using CGIERPData.Enums;
using CGIERPData.Models.EmbeddedObjects;

namespace CGIERPData.Models.QHSEModels;

public partial class QhseAssignmentModel : RealmObject
{
    [PrimaryKey]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public ItemModel Item { get; set; }

    // Utilizing the Embedded Object for clean schema design
    public RecipientDetails Recipient { get; set; }

    public int QuantityAssigned { get; set; }
    public string Reason { get; set; }

    public DateTimeOffset AssignmentDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ReturnDate { get; set; }
    public bool IsReturned { get; set; } = false;
}
