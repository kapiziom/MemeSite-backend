using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MemeSite.Api.ViewModels
{
    public class AddFavouriteVM
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public int MemeId { get; set; }
    }
}
