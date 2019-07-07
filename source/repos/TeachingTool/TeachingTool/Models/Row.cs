using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingTool.Models
{
    public class Row
    {
        [JsonProperty("firstCell")]
        public string FirstCell { get; set; }

        [JsonProperty("secondCell")]
        public string SecondCell { get; set; }

        [JsonProperty("thirdCell")]
        public string ThirdCell { get; set; }

    }
}
