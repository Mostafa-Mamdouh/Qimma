using Newtonsoft.Json;


namespace PRJ.Application.Warppers
{
    public class PagingResponse<T>
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public List<string>? Errors { get; set; }
    }
    public class EnquiryPagingResponse<T>
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public List<string>? Errors { get; set; }
        public List<string> SealedIds { get; set; }
        public List<string> UnSealedIds { get; set; }
        public List<string> VariableUnSealedIds { get; set; }
    }
}

