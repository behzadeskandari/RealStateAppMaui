using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Models
{
    public class BookMark
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("user_Id")]
        public bool UserId { get; set; }

        [JsonProperty("propertyId")]
        public bool PropertyId { get; set; }

    }
}
