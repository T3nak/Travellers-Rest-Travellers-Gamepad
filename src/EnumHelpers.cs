using BepInEx.Logging;

namespace Pixelnaut.TravellersGamepad;

public static class EnumHelpers
{
    public static LogLevel RemoveFlag(this LogLevel level, LogLevel flag)
    {
        return level & ~flag;
    }
}
