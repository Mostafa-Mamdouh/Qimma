using Newtonsoft.Json;
using PRJ.Application.Warppers.models;
using Column = PRJ.Application.Warppers.models.Column;

namespace PRJ.Application.Warppers
{
    public class EnquiryPagingRequest
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
        public int? lang { get; set; }

        public List<string> Entities { get; set; }
        public List<string> Facilities { get; set; }

        public List<string> Licenses { get; set; }

        public List<string> Permits { get; set; }

        public List<string> Practises { get; set; }

        public List<string> Ids { get; set; }

    }
}
