using Newtonsoft.Json;

namespace PRJ.Application.Warppers.models
{
    public class Search
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }
    }
}
