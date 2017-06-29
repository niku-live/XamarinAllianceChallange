using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace XamarinBackendService.DataObjects
{
    public class Weapon : BaseDataObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

    }
}