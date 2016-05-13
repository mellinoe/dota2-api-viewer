using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class AbilityUpgrade
    {
        [JsonProperty(PropertyName = "ability")]
        public int AbilityID { get; set; }
        [JsonProperty(PropertyName = "time")]
        public int Time { get; set; }
        [JsonProperty(PropertyName = "level")]
        public int HeroLevel { get; set; }
    }
}
