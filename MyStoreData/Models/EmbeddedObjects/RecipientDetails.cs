using System;

namespace MyStoreData.Models.EmbeddedObjects;


// Used in Assignments
public partial class RecipientDetails : EmbeddedObject
{
    public string Name { get; set; }
    public string Company { get; set; }
    public bool IsInternalColleague { get; set; }
    public string ContactInfo { get; set; }
}
