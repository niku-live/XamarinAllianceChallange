using System.Collections.Generic;

namespace XamarinBackendService.DataObjects
{
    public class Character : BaseDataObject
    {
        public string? Name { get; set; }

        public string? Biography { get; set; }

        public string? DatabankUrl { get; set; }

        public string? ImageUrl { get; set; }

        public string? Gender { get; set; }

        public float Height { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; } = new List<Weapon>();

        public virtual ICollection<Movie> Appearances { get; set; } = new List<Movie>();
    }
}
