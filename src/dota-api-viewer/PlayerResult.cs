namespace DotaApiViewer
{
    public class PlayerResult
    {
        public int AccountID { get; private set; }
        public int PlayerSlot { get; private set; }
        public int HeroID { get; private set; }
        public int Item0 { get; private set; }
        public int Item1 { get; private set; }
        public int Item2 { get; private set; }
        public int Item3 { get; private set; }
        public int Item4 { get; private set; }
        public int Item5 { get; private set; }
        public int Kills { get; private set; }
        public int Deaths { get; private set; }
        public int Assists { get; private set; }
        public int LeaverStatus { get; private set; }
        public int LastHits { get; private set; }
        public int Denies { get; private set; }
        public int GoldPerMinute { get; private set; }
        public int ExperiencePerMinute { get; private set; }
        public int Level { get; private set; }
    }
}