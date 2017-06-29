using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace XamarinAllianceApp.Models
{
    public class Character
    {
        Int32 id;
        string name;
        string biography;
        string gender;
        float height;
        string databankUrl;
        ICollection<Weapon> weapons;
        ICollection<Movie> appearances;

        [JsonProperty(PropertyName = "id")]
        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "gender")]
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        [JsonProperty(PropertyName = "biography")]
        public string Biography
        {
            get { return biography; }
            set { biography = value; }
        }

        [JsonProperty(PropertyName = "height")]
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        [JsonProperty(PropertyName = "databankUrl")]
        public string DatabankUrl
        {
            get { return databankUrl; }
            set { databankUrl = value; }
        }

        [JsonProperty(PropertyName = "weapons")]
        public ICollection<Weapon> Weapons
        {
            get { return weapons; }
            set { weapons = value; }
        }

        [JsonProperty(PropertyName = "appearances")]
        public ICollection<Movie> Appearances
        {
            get { return appearances; }
            set { appearances = value; }
        }

        public string Version { get; set; }
    }



    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public bool deleted { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string version { get; set; }
        public string id { get; set; }
        public object remoteId { get; set; }
        public int height { get; set; }
        public string gender { get; set; }
        public string imageUrl { get; set; }
        public string databankUrl { get; set; }
        public string biography { get; set; }
        public string name { get; set; }
        public Appearance[] appearances { get; set; }
        public Weapon[] weapons { get; set; }
    }

    public class Appearance
    {
        public string title { get; set; }
        public string description { get; set; }
        public object remoteId { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool deleted { get; set; }
    }





}
