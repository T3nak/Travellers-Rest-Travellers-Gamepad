using System.Collections.Generic;

namespace Pixelnaut.TravellersGamepad;

internal readonly record struct GamepadButtonMap(
    bool IsEnabled,
    GamepadActionButton Button1,
    GamepadActionButton Button2,
    GamepadActionButton Button3,
    GamepadActionButton Button4
)
{
    public static readonly Dictionary<GamepadActionButton, ActionType[]> Defaults = new()
    {
        { GamepadActionButton.Button1, [ActionType.Use, ActionType.UIAddRemove] },
        { GamepadActionButton.Button2, [ActionType.Cancel, ActionType.UIBack] },
        { GamepadActionButton.Button3, [ActionType.Interact, ActionType.UIInteract] },
        { GamepadActionButton.Button4, [ActionType.Select, ActionType.UISelectGamepad] }
    };

    public Dictionary<GamepadActionButton, ActionType[]> Effective => new()
    {
        { GamepadActionButton.Button1, Defaults[Button1] },
        { GamepadActionButton.Button2, Defaults[Button2] },
        { GamepadActionButton.Button3, Defaults[Button3] },
        { GamepadActionButton.Button4, Defaults[Button4] },
    };
}
