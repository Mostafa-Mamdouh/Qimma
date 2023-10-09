using Newtonsoft.Json;
using PRJ.Application.Warppers.models;
using Column = PRJ.Application.Warppers.models.Column;

namespace PRJ.Application.Warppers
{
    public class PagingRequest
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty(PropertyName = "order")]
        public IList<Order> Order { get; set; }

        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "search")]
        public Search Search { get; set; }

        [JsonProperty(PropertyName = "searchCriteria")]
        public SearchCriteria SearchCriteria { get; set; }
        public string extra { get; set; }
        public string extra2 { get; set; }
        public int? data { get; set; }
        public string Id { get; set; }
        public int? lang { get; set; }
        public string ForPage { get; set; }
    }
}
