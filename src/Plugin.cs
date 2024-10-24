using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Pixelnaut.TravellersGamepad;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public Plugin()
    {
        Logger = base.Logger;

        Settings = new Settings(
            LogLevels: Config.Bind(
                section: "Logging",
                key: "Levels",
                defaultValue: LogLevel.All.RemoveFlag(LogLevel.Debug),
                description: "Which log levels to show in the console output."
            ).Value,

            GamepadLayout: Config.Bind(
                section: "General",
                key: "Gamepad Layout",
                defaultValue: GamepadButtonLayoutStyle.Auto,
                description: new string[]
                {
                    "Forces in-game buttons to conform to this layout.",
                    $"{GamepadButtonLayoutStyle.Auto} - lets the game choose your button layout automatically",
                    $"{GamepadButtonLayoutStyle.Custom} - uses the bindings defined in the Custom Layout section",
                    $"{GamepadButtonLayoutStyle.Nintendo} - uses Nintendo-style buttons",
                    $"{GamepadButtonLayoutStyle.PlayStation} - uses PlayStation-style buttons",
                    $"{GamepadButtonLayoutStyle.Xbox} - uses Xbox-style buttons",
                }.Join(delimiter: Environment.NewLine)
            ).Value,

            CustomLayout: new GamepadButtonLayout(
                Button1: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 1",
                    defaultValue: GamepadButton.XboxY,
                    description: new string[] { "Nintendo: X", "PlayStation: Triangle", "Xbox: Y" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button2: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 2",
                    defaultValue: GamepadButton.XboxB,
                    description: new string[] { "Nintendo: A", "PlayStation: Circle", "Xbox: B" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button3: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 3",
                    defaultValue: GamepadButton.XboxA,
                    description: new string[] { "Nintendo: B", "PlayStation: Cross", "Xbox: A" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button4: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 4",
                    defaultValue: GamepadButton.XboxX,
                    description: new string[] { "Nintendo: Y", "PlayStation: Square", "Xbox: X" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button5: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 5",
                    defaultValue: GamepadButton.XboxLeftBumper,
                    description: new string[] { "Nintendo: L", "PlayStation: L1", "Xbox: Left Bumper (LB)" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button6: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 6",
                    defaultValue: GamepadButton.XboxRightBumper,
                    description: new string[] { "Nintendo: R", "PlayStation: R1", "Xbox: Right Bumper (RB)" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button7: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 7",
                    defaultValue: GamepadButton.XboxLeftTrigger,
                    description: new string[] { "Nintendo: ZL", "PlayStation: L2", "Xbox: Left Trigger (LT)" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button8: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 8",
                    defaultValue: GamepadButton.XboxRightTrigger,
                    description: new string[] { "Nintendo: ZR", "PlayStation: R2", "Xbox: Right Trigger (RT)" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button9: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 9",
                    defaultValue: GamepadButton.XboxView,
                    description: new string[] { "Nintendo: - (minus)", "PlayStation: Create", "Xbox: View" }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button10: Config.Bind(
                    section: "Custom Layout",
                    key: "Button 10",
                    defaultValue: GamepadButton.XboxMenu,
                    description: new string[] { "Nintendo: + (plus)", "PlayStation: Options", "Xbox: Menu" }.Join(delimiter: Environment.NewLine)
                ).Value
            ),

            GamepadMap: new GamepadButtonMap(
                IsEnabled: Config.Bind(
                    section: "Mapping",
                    key: "Enable Custom Mappings",
                    defaultValue: false,
                    description: "If enabled, these mappings will be applied; otherwise, they will be ignored."
                ).Value,
                Button1: Config.Bind(
                    section: "Mapping",
                    key: "Button1",
                    defaultValue: GamepadActionButton.Button1,
                    description: new string[]
                    {
                        "Redirects action button 1 to any other action button.",
                        "Nintendo: X",
                        "PlayStation: Triangle",
                        "Xbox: Y",
                        "E.g. Nintendo controllers typically have their X button at the same physical spot as the Xbox controller's Y button. Therefore, only by redirecting Button 1 to Button 4, and Button 4 to Button 1, can a Nintendo controller be used effectively.",
                    }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button2: Config.Bind(
                    section: "Mapping",
                    key: "Button2",
                    defaultValue: GamepadActionButton.Button2,
                    description: new string[]
                    {
                        "Redirects action button 2 to any other action button.",
                        "Nintendo: A",
                        "PlayStation: Circle",
                        "Xbox: B",
                        "E.g. Nintendo controllers typically have their A button at the same physical spot as the Xbox controller's B button. Therefore, only by redirecting Button 2 to Button 3, and Button 3 to Button 2, can a Nintendo controller be used effectively.",
                    }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button3: Config.Bind(
                    section: "Mapping",
                    key: "Button3",
                    defaultValue: GamepadActionButton.Button3,
                    description: new string[]
                    {
                        "Redirects action button 3 to any other action button.",
                        "Nintendo: B",
                        "PlayStation: Cross",
                        "Xbox: A",
                        "E.g. Nintendo controllers typically have their B button at the same physical spot as the Xbox controller's A button. Therefore, only by redirecting Button 3 to Button 2, and Button 2 to Button 3, can a Nintendo controller be used effectively.",
                    }.Join(delimiter: Environment.NewLine)
                ).Value,
                Button4: Config.Bind(
                    section: "Mapping",
                    key: "Button4",
                    defaultValue: GamepadActionButton.Button4,
                    description: new string[]
                    {
                        "Redirects action button 4 to any other action button.",
                        "Nintendo: Y",
                        "PlayStation: Square",
                        "Xbox: X",
                        "E.g. Nintendo controllers typically have their Y button at the same physical spot as the Xbox controller's X button. Therefore, only by redirecting Button 4 to Button 1, and Button 1 to Button 4, can a Nintendo controller be used effectively.",
                    }.Join(delimiter: Environment.NewLine)
                ).Value
            )
        );

        EffectiveLayout = Settings.GamepadLayout switch
        {
            GamepadButtonLayoutStyle.Custom => Settings.CustomLayout,
            GamepadButtonLayoutStyle.Nintendo => new GamepadButtonLayout(
                Button1: GamepadButton.NintendoX,
                Button2: GamepadButton.NintendoA,
                Button3: GamepadButton.NintendoB,
                Button4: GamepadButton.NintendoY,
                Button5: GamepadButton.NintendoL,
                Button6: GamepadButton.NintendoR,
                Button7: GamepadButton.NintendoZL,
                Button8: GamepadButton.NintendoZR,
                Button9: GamepadButton.NintendoMinus,
                Button10: GamepadButton.NintendoPlus
            ),
            GamepadButtonLayoutStyle.PlayStation => new GamepadButtonLayout(
                Button1: GamepadButton.PlayStationTriangle,
                Button2: GamepadButton.PlayStationCircle,
                Button3: GamepadButton.PlayStationCross,
                Button4: GamepadButton.PlayStationSquare,
                Button5: GamepadButton.PlayStationL1,
                Button6: GamepadButton.PlayStationR1,
                Button7: GamepadButton.PlayStationL2,
                Button8: GamepadButton.PlayStationR2,
                Button9: GamepadButton.PlayStationCreate,
                Button10: GamepadButton.PlayStationOptions
            ),
            GamepadButtonLayoutStyle.Xbox => new GamepadButtonLayout(
                Button1: GamepadButton.XboxY,
                Button2: GamepadButton.XboxB,
                Button3: GamepadButton.XboxA,
                Button4: GamepadButton.XboxX,
                Button5: GamepadButton.XboxLeftBumper,
                Button6: GamepadButton.XboxRightBumper,
                Button7: GamepadButton.XboxLeftTrigger,
                Button8: GamepadButton.XboxRightTrigger,
                Button9: GamepadButton.XboxView,
                Button10: GamepadButton.XboxMenu
            ),
            _ => null
        };

        EffectiveMap = Settings.GamepadMap.IsEnabled
            ? Settings.GamepadMap
            : Settings.GamepadLayout switch
            {
                GamepadButtonLayoutStyle.Nintendo => new GamepadButtonMap(
                    IsEnabled: Settings.GamepadMap.IsEnabled,
                    Button1: GamepadActionButton.Button4,
                    Button2: GamepadActionButton.Button3,
                    Button3: GamepadActionButton.Button2,
                    Button4: GamepadActionButton.Button1
                ),
                _ => null
            };
    }

    private static bool DidRebindInputs = false;

    private static Harmony? Harmony;

    private static MethodInfo? ReentrantMethod;

    private static Settings Settings;

    private static GamepadButtonLayout? EffectiveLayout;

    private static GamepadButtonMap? EffectiveMap;

    internal static new ManualLogSource? Logger;

    private static bool ButtonsContext_GetSpriteWithElementName_Prefix(object[] __args, ref Sprite __result)
    {
        if (ReentrantMethod == default || !EffectiveLayout.HasValue)
        {
            return true;
        }

        string? buttonName = __args.OfType<string>().FirstOrDefault();

        if (buttonName == default)
        {
            return true;
        }

        string gamepadAction = buttonName switch
        {
            GamepadButtonNames.XboxY or GamepadButtonNames.PlayStationTriangle => GamepadButtonNames.GetName(EffectiveLayout.Value.Button1),
            GamepadButtonNames.XboxB or GamepadButtonNames.PlayStationCircle => GamepadButtonNames.GetName(EffectiveLayout.Value.Button2),
            GamepadButtonNames.XboxA or GamepadButtonNames.PlayStationCross => GamepadButtonNames.GetName(EffectiveLayout.Value.Button3),
            GamepadButtonNames.XboxX or GamepadButtonNames.PlayStationSquare => GamepadButtonNames.GetName(EffectiveLayout.Value.Button4),
            GamepadButtonNames.XboxLeftBumper or GamepadButtonNames.PlayStationL1 => GamepadButtonNames.GetName(EffectiveLayout.Value.Button5),
            GamepadButtonNames.XboxRightBumper or GamepadButtonNames.PlayStationR1 => GamepadButtonNames.GetName(EffectiveLayout.Value.Button6),
            GamepadButtonNames.XboxLeftTrigger or GamepadButtonNames.PlayStationL2 => GamepadButtonNames.GetName(EffectiveLayout.Value.Button7),
            GamepadButtonNames.XboxRightTrigger or GamepadButtonNames.PlayStationR2 => GamepadButtonNames.GetName(EffectiveLayout.Value.Button8),
            GamepadButtonNames.XboxView or GamepadButtonNames.PlayStationCreate => GamepadButtonNames.GetName(EffectiveLayout.Value.Button9),
            GamepadButtonNames.XboxMenu or GamepadButtonNames.PlayStationOptions => GamepadButtonNames.GetName(EffectiveLayout.Value.Button10),
            _ => buttonName
        };

        Log($"Calling original GetSpriteWithElementName with {gamepadAction} (used to be {buttonName}) because gamepad layout style is {Settings.GamepadLayout}");

        __args[Array.IndexOf(__args, buttonName)] = gamepadAction;
        __result = (Sprite)ReentrantMethod.Invoke(null, __args);

        return false;
    }

    private void Awake()
    {
        if (EffectiveLayout.HasValue)
        {
            Harmony = Harmony.CreateAndPatchAll(typeof(Plugin));

            var original = AccessTools.Method(typeof(ButtonsContext), nameof(ButtonsContext.GetSpriteWithElementName));
            var prefix = AccessTools.Method(typeof(Plugin), nameof(ButtonsContext_GetSpriteWithElementName_Prefix));
            ReentrantMethod = Harmony.Patch(original);
            Harmony.Patch(original, prefix: new HarmonyMethod(prefix));
        }

        if (!EffectiveMap.HasValue)
        {
            enabled = false;
        }

        Log($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!", level: LogLevel.Info);
    }

    private void Update()
    {
        if (!EffectiveMap.HasValue)
        {
            enabled = false;
            return;
        }

        if (!ReInput.isReady)
        {
            return;
        }

        Player? player = ReInput.players?.GetPlayers()?.FirstOrDefault();

        if (player == default)
        {
            return;
        }

        ActionType[] actionTypes = GamepadButtonMap.Defaults
            .SelectMany(d => d.Value)
            .Select(a => a)
            .ToArray();

        if (!DidRebindInputs)
        {
            foreach (Controller controller in player.controllers.Controllers)
            {
                foreach (ControllerMap controllerMap in player.controllers.maps.GetMaps(controller))
                {
                    if (controllerMap is not JoystickMap joystickMap)
                    {
                        continue;
                    }

                    foreach (ActionElementMap actionElementMap in controllerMap.AllMaps)
                    {
                        InputAction action = ReInput.mapping.GetAction(actionElementMap.actionId);

                        if (
                            actionElementMap.elementType != ControllerElementType.Button
                            || !Enum.TryParse(action.name, ignoreCase: true, out ActionType actionType)
                            || !actionTypes.Contains(actionType)
                            || !actionElementMap.enabled
                        )
                        {
                            continue;
                        }

                        KeyValuePair<GamepadActionButton, ActionType[]> actionDefault = GamepadButtonMap
                            .Defaults
                            .First(d => d.Value.Contains(actionType));

                        int index = Array.IndexOf(actionDefault.Value, actionType);

                        ActionType newActionType = EffectiveMap.Value.Effective[actionDefault.Key][index];

                        if (newActionType == actionType)
                        {
                            continue;
                        }

                        var newAction = ReInput.mapping.GetAction(newActionType.ToString());

                        Log($"{actionElementMap.elementType} {actionElementMap.elementIndex} will be rebound from {action.name} ({actionElementMap.actionId}) to {newAction.name} ({newAction.id})");

                        actionElementMap.actionId = newAction.id;
                    }
                }
            }
        }

        DidRebindInputs = true;
        enabled = false;
    }

    private void OnDestroy()
    {
        Harmony?.UnpatchSelf();
    }

    internal static void Log(string message, LogLevel level = LogLevel.Debug)
    {
        if (
            Settings.LogLevels != default
            && Logger != default
            && Settings.LogLevels.HasFlag(level)
        )
        {
            Logger.Log(level, message);
        }
    }
}
