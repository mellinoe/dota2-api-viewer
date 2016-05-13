using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class AdditionalUnit
    {
        [JsonProperty(PropertyName = "unitname")]
        public string UnitName { get; set; }
        [JsonProperty(PropertyName = "item_0")]
        public int Item0 { get; set; }
        [JsonProperty(PropertyName = "item_1")]
        public int Item1 { get; set; }
        [JsonProperty(PropertyName = "item_2")]
        public int Item2 { get; set; }
        [JsonProperty(PropertyName = "item_3")]
        public int Item3 { get; set; }
        [JsonProperty(PropertyName = "item_4")]
        public int Item4 { get; set; }
        [JsonProperty(PropertyName = "item_5")]
        public int Item5 { get; set; }
    }
}
