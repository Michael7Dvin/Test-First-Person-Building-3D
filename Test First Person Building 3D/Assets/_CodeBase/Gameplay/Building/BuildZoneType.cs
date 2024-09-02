using System;

namespace _CodeBase.Gameplay.Building
{
    [Flags]
    public enum BuildZoneType
    {
        Floor = 1 << 0,
        Wall = 1 << 1,
        Box = 1 << 2,
    }
}