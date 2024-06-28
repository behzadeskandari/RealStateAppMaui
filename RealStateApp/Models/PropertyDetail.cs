using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Models
{
    public class PropertyDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("phone")]
        public int Phone { get; set; }

        [JsonProperty("bookmarks")]
        public BookMark Bookmarks { get; set; }

    }
}
