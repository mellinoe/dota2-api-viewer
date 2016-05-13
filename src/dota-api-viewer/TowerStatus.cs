using System;

namespace DotaApiViewer
{
    [Flags]
    public enum TowerStatus
    {
        TopTier1 = 1 << 0,
        TopTier2 = 1 << 1,
        TopTier3 = 1 << 2,
        MiddleTier1 = 1 << 3,
        MiddleTier2 = 1 << 4,
        MiddleTier3 = 1 << 5,
        BottomTier1 = 1 << 6,
        BottomTier2 = 1 << 7,
        BottomTier3 = 1 << 8,
        AncientTop = 1 << 9,
        AncientBottom = 1 << 10
    }
}