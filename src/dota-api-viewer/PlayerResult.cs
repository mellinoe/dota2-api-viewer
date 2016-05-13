using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class PlayerResult
    {
        [JsonProperty(PropertyName = "account_id")]
        public int SteamID { get; set; }
        [JsonProperty(PropertyName = "player_slot")]
        public byte PlayerSlot { get; set; }
        [JsonProperty(PropertyName = "hero_id")]
        public int HeroID { get; set; }
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
        [JsonProperty(PropertyName = "kills")]
        public int Kills { get; set; }
        [JsonProperty(PropertyName = "deaths")]
        public int Deaths { get; set; }
        [JsonProperty(PropertyName = "assists")]
        public int Assists { get; set; }
        [JsonProperty(PropertyName = "leaver_status")]
        public LeaverStatus LeaverStatus { get; set; }
        [JsonProperty(PropertyName = "last_hits")]
        public int LastHits { get; set; }
        [JsonProperty(PropertyName = "denies")]
        public int Denies { get; set; }
        [JsonProperty(PropertyName = "gold_per_min")]
        public int GoldPerMinute { get; set; }
        [JsonProperty(PropertyName = "xp_per_min")]
        public int ExperiencePerMinute { get; set; }
        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }
        [JsonProperty(PropertyName = "gold")]
        public int Gold { get; set; }
        [JsonProperty(PropertyName = "gold_spent")]
        public int GoldSpent { get; set; }
        [JsonProperty(PropertyName = "hero_damage")]
        public int HeroDamage { get; set; }
        [JsonProperty(PropertyName = "tower_damage")]
        public int TowerDamage { get; set; }
        [JsonProperty(PropertyName = "hero_healing")]
        public int HeroHealing { get; set; }
        [JsonProperty(PropertyName = "ability_upgrades")]
        public AbilityUpgrade[] AbilityUpgrades { get; set; }

    }
}