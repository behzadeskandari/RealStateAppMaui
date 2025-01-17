﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }



        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;
        
        [JsonProperty("properties")]
        public string Properties { get; set; }

    }
}
