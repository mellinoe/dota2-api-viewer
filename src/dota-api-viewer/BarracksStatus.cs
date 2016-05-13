using System;

namespace DotaApiViewer
{
    [Flags]
    public enum BarracksStatus
    {
        TopMelee = 1 << 0,
        TopRanged = 1 << 1,
        MiddleMelee = 1 << 2,
        MiddleRanged = 1 << 3,
        BottomMelee = 1 << 4,
        BottomRanged = 1 << 5
    }
}