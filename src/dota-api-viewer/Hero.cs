namespace DotaApiViewer
{
    /// <summary>
    /// Describes a playable Dota 2 hero.
    /// </summary>
    public class Hero
    {
        /// <summary>
        /// The internal name of the hero.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The hero's internal ID value.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The localized, friendly name of the hero.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "localized_name")]
        public string LocalizedName { get; set; }

        public override string ToString()
        {
            return $"{LocalizedName}, [{Name}, id:{ID}]";
        }
    }
}
