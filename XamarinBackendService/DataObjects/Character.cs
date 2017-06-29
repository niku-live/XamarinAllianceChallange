using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace XamarinBackendService.DataObjects
{
    public class Character : BaseDataObject
    {
        /// <summary>
        /// Gets or sets the character name.
        /// </summary>
        /// <value>The character's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        /// <value>The biography.</value>
        public string Biography { get; set; }

        /// <summary>
        /// This is the Starwars Databank URL
        /// </summary>
        /// <value>The databank URL.</value>
        public string DatabankUrl { get; set; }

        /// <summary>
        /// Gets or sets the character image URL
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Gets or sets the gender
        /// </summary>
        /// <value>The gender (male or female)</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        /// <value>The height in cm</value>
        public float Height { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }

        public virtual ICollection<Movie> Appearances { get; set; }

    }
}