using System.Collections.Generic;

namespace Pixelnaut.TravellersGamepad;


/// <summary>
/// <see cref="DualShockInfo"/> and <see cref="XboxInfo"/>.
/// </summary>
internal static class GamepadButtonNames
{
    public const string NintendoX = "X";
    public const string NintendoA = "A";
    public const string NintendoB = "B";
    public const string NintendoY = "Y";
    public const string NintendoL = "Left Shoulder";
    public const string NintendoR = "Right Shoulder";
    public const string NintendoZL = "Left Trigger";
    public const string NintendoZR = "Right Trigger";
    public const string NintendoMinus = "Back";
    public const string NintendoPlus = "Start";

    public const string PlayStationTriangle = "Triangle";
    public const string PlayStationCircle = "Circle";
    public const string PlayStationCross = "Cross";
    public const string PlayStationSquare = "Square";
    public const string PlayStationL1 = "L1";
    public const string PlayStationR1 = "R1";
    public const string PlayStationL2 = "L2";
    public const string PlayStationR2 = "R2";
    public const string PlayStationCreate = "Share";
    public const string PlayStationOptions = "Options";

    public const string XboxY = "Y";
    public const string XboxB = "B";
    public const string XboxA = "A";
    public const string XboxX = "X";
    public const string XboxLeftBumper = "Left Shoulder";
    public const string XboxRightBumper = "Right Shoulder";
    public const string XboxLeftTrigger = "Left Trigger";
    public const string XboxRightTrigger = "Right Trigger";
    public const string XboxView = "Back";
    public const string XboxMenu = "Start";

    public static readonly Dictionary<GamepadButton, string> ButtonNamesMap = new()
    {
        { GamepadButton.NintendoX, NintendoX },
        { GamepadButton.NintendoA, NintendoA },
        { GamepadButton.NintendoB, NintendoB },
        { GamepadButton.NintendoY, NintendoY },
        { GamepadButton.NintendoL, NintendoL },
        { GamepadButton.NintendoR, NintendoR },
        { GamepadButton.NintendoZL, NintendoZL },
        { GamepadButton.NintendoZR, NintendoZR },
        { GamepadButton.NintendoMinus, NintendoMinus },
        { GamepadButton.NintendoPlus, NintendoPlus },

        { GamepadButton.PlayStationTriangle, PlayStationTriangle },
        { GamepadButton.PlayStationCircle, PlayStationCircle },
        { GamepadButton.PlayStationCross, PlayStationCross },
        { GamepadButton.PlayStationSquare, PlayStationSquare },
        { GamepadButton.PlayStationL1, PlayStationL1 },
        { GamepadButton.PlayStationR1, PlayStationR1 },
        { GamepadButton.PlayStationL2, PlayStationL2 },
        { GamepadButton.PlayStationR2, PlayStationR2 },
        { GamepadButton.PlayStationCreate, PlayStationCreate },
        { GamepadButton.PlayStationOptions, PlayStationOptions },

        { GamepadButton.XboxY, XboxY },
        { GamepadButton.XboxB, XboxB },
        { GamepadButton.XboxA, XboxA },
        { GamepadButton.XboxX, XboxX },
        { GamepadButton.XboxLeftBumper, XboxLeftBumper },
        { GamepadButton.XboxRightBumper, XboxRightBumper },
        { GamepadButton.XboxLeftTrigger, XboxLeftTrigger },
        { GamepadButton.XboxRightTrigger, XboxRightTrigger },
        { GamepadButton.XboxView, XboxView },
        { GamepadButton.XboxMenu, XboxMenu },
    };

    public static string GetName(GamepadButton button) => ButtonNamesMap[button];
}
