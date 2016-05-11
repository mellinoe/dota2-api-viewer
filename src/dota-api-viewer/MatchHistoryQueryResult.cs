using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class MatchHistoryQueryResult
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "num_results")]
        public int NumResults { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "results_remaining")]
        public int ResultsRemaining { get; set; }

        [JsonProperty(PropertyName = "matches")]
        public MatchSummary[] Matches { get; set; }
    }

    public class MatchSummary
    {
        [JsonProperty(PropertyName = "match_id")]
        public string MatchID { get; set; }

        [JsonProperty(PropertyName = "match_seq_num")]
        public string MatchSequenceNumber { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public uint StartTime { get; set; }

        [JsonProperty(PropertyName = "lobby_type")]
        public LobbyType LobbyType { get; set; }

        [JsonProperty(PropertyName = "radiant_team_id")]
        public int RadiantTeamID { get; set; }

        [JsonProperty(PropertyName = "dire_team_id")]
        public int DireTeamID { get; set; }

        [JsonProperty(PropertyName = "players")]
        public PlayerSummary[] Players { get; set; }
    }

    public class PlayerSummary
    {
        [JsonProperty(PropertyName = "account_id")]
        public string SteamID { get; set; }

        [JsonProperty(PropertyName = "player_slot")]
        public int PlayerSlot { get; set; }

        [JsonProperty(PropertyName = "hero_id")]
        public int HeroID { get; set; }
    }
}
