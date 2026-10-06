using MyStoreData.Enums;
using MyStoreData.Models.EmbeddedObjects;

namespace MyStoreData.Models.QHSEModels
{
    public partial class QhseVerificationModel : RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        public string Client { get; set; }
        public string Designation { get; set; }
        public string SerialNumberAndModel { get; set; }

        public DateTimeOffset LastVerificationDate { get; set; }
        public DateTimeOffset NextVerificationDate { get; set; }
        public string MincomAgentName { get; set; }

        // Status tracking
        public int CurrentStatusCode { get; set; } = (int)VerificationStatus.Pending;

        // Embedded List: An audit trail of every status change (Extremely useful for ERPs)
        public IList<StatusHistoryLog> StatusAuditTrail { get; } = null!;

        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}