using Newtonsoft.Json;

namespace DotaApiViewer
{
    public class MatchDetailsQueryResult
    {
        [JsonProperty(PropertyName = "players")]
        public PlayerResult[] PlayerResults { get; set; }
        [JsonProperty(PropertyName = "radiant_win")]
        public bool RadiantWin { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }
        [JsonProperty(PropertyName = "start_time")]
        public int StartTime { get; set; }
        [JsonProperty(PropertyName = "match_id")]
        public long MatchID { get; set; }
        [JsonProperty(PropertyName = "match_seq_number")]
        public int SequenceNumber { get; set; }
        [JsonProperty(PropertyName = "tower_status_radiant")]
        public TowerStatus TowerStatusRadiant { get; set; }
        [JsonProperty(PropertyName = "tower_status_dire")]
        public TowerStatus TowerStatusDire { get; set; }
        [JsonProperty(PropertyName = "barracks_status_radiant")]
        public BarracksStatus BarracksStatusRadiant { get; set; }
        [JsonProperty(PropertyName = "barracks_status_dire")]
        public BarracksStatus BarracksStatusDire { get; set; }
        [JsonProperty(PropertyName = "cluster")]
        public int Cluster { get; set; }
        [JsonProperty(PropertyName = "first_blood_time")]
        public int FirstBloodTime { get; set; }
        [JsonProperty(PropertyName = "lobby_type")]
        public LobbyType LobbyType { get; set; }
        [JsonProperty(PropertyName = "human_players")]
        public int NumHumanPlayers { get; set; }
        [JsonProperty(PropertyName = "league_id")]
        public int LeagueID { get; set; }
        [JsonProperty(PropertyName = "positive_votes")]
        public int PositiveVotes { get; set; }
        [JsonProperty(PropertyName = "negative_votes")]
        public int NegativeVote { get; set; }
        [JsonProperty(PropertyName = "game_mode")]
        public GameMode GameMode { get; set; }
        [JsonProperty(PropertyName = "flags")]
        public int Flags { get; set; }
        [JsonProperty(PropertyName = "engine")]
        public int Engine { get; set; }
        [JsonProperty(PropertyName = "radiant_score")]
        public int RadiantScore { get; set; }
        [JsonProperty(PropertyName = "dire_score")]
        public int DireScore { get; set; }
    }
}
