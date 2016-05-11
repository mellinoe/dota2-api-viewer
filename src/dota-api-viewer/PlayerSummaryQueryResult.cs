using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class PlayerSummaryQueryResult
    {
        public SteamPlayerSummary[] Players { get; set; }
    }

    public class SteamPlayerSummary
    {
        public string SteamID { get; set; }
        public int CommunityVisibilityState { get; set; }
        public int ProfileState { get; set; }
        public string PersonaName { get; set; }
        [JsonProperty(PropertyName = "lastlogoff")]
        public uint LastLogOffTime { get; set; }
        public string ProfileUrl { get; set; }
        public string Avatar { get; set; }
        public string AvatarMedium { get; set; }
        public string AvatarFull { get; set; }
        public int PersonaState { get; set; }
        public string PrimaryClanID { get; set; }
        public int TimeCreated { get; set; }
        public int PersonaStateFlags { get; set; }
    }
}
