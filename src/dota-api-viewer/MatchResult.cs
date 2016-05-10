namespace DotaApiViewer
{
    public class MatchResult
    {
        public PlayerResult[] PlayerResults { get; private set; }

        public bool RadiantWin { get; private set; }
        public int Duration { get; private set; }
        public int StartTime { get; private set; }
        public int MatchID { get; private set; }
        public int MatchSequenceNumber { get; private set; }
        public int RadiantTowerStatus { get; private set; }
        public int DireTowerStatus { get; private set; }
        public int RadiantBarracksStatus { get; private set; }
        public int DireBarracksStatus { get; private set; }
        public int ServerCluster { get; private set; }
        public int FirstBloodTime { get; private set; }
        public LobbyType LobbyType { get; private set; }
        public int HumanPlayerCount { get; private set; }
        public int LeagueID { get; private set; }
        public int PositiveVotes { get; private set; }
        public int NegativeVotes { get; private set; }
        public GameMode GameMode { get; private set; }
        public int Flags { get; private set; }
        public int Engine { get; private set; }
        public int RadiantScore { get; private set; }
        public int DireScore { get; private set; }
    }
}
