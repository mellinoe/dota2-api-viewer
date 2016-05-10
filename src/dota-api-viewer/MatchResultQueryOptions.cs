namespace DotaApiViewer
{
    public class MatchHistoryQueryOptions
    {
        public int HeroID = -1;
        public GameMode GameMode = GameMode.Any;
        public SkillLevel SkillLevel = SkillLevel.Any;
        public int MinDate = -1;
        public int MaxDate = -1;
        public int MinPlayers = -1;
        public string AccountID = null;
        public string LeagueID = null;
        public string StartAtMatchID = null;
        public int MatchesRequested = -1;
        public bool TournamentGamesOnly = false;
    }
}
