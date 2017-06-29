using Microsoft.Azure.Mobile.Server;
using System;

namespace XamarinBackendService.DataObjects
{
    public class BaseDataObject : EntityData
    {
        public BaseDataObject ()
        {
            Id = Guid.NewGuid().ToString();
        }

        public String RemoteId { get; set; }
    }
}
