using System;

namespace MyStoreData.Models.EmbeddedObjects
{

    // Used to track exactly WHEN a status changed (Audit Trail)
    public partial class StatusHistoryLog : EmbeddedObject
    {
        public int StatusCode { get; set; }
        public UserModel ChangedBy { get; set; } // Username of who changed it
        public DateTimeOffset DateChanged { get; set; } = DateTimeOffset.UtcNow;
    }
}