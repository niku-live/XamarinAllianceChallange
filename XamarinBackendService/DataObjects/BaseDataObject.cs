using System;
using System.ComponentModel.DataAnnotations;

namespace XamarinBackendService.DataObjects
{
    public class BaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }

        public string RemoteId { get; set; }
    }
}
