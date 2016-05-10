using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class Item
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        [JsonProperty(PropertyName = "secret_shop")]
        public bool SoldAtSecretShop { get; set; }

        [JsonProperty(PropertyName = "side_shop")]
        public bool SoldAtSideShop { get; set; }

        [JsonProperty(PropertyName = "recipe")]
        public bool IsRecipe { get; set; }

        [JsonProperty(PropertyName = "localized_name")]
        public string LocalizedName { get; set; }

        public override string ToString()
        {
            return $"{LocalizedName}: [{Name}, id:{ID}], {Cost} Gold, Secret Shop {SoldAtSecretShop.ToCheckbox()}, Side Shop {SoldAtSideShop.ToCheckbox()}, Recipe {IsRecipe.ToCheckbox()}";
        }
    }
}