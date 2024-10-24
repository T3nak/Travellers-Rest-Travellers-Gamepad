using BepInEx.Logging;

namespace Pixelnaut.TravellersGamepad;

internal readonly record struct Settings(
    LogLevel LogLevels,
    GamepadButtonLayoutStyle GamepadLayout,
    GamepadButtonLayout CustomLayout,
    GamepadButtonMap GamepadMap
);
