using HarmonyLib;
using NeosModLoader;
using FrooxEngine.LogiX;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace DisableCustomConnectPoint
{
    public class DisableCustomConnectPoint : NeosMod
    {
        public override string Name => BuildInfo.Name;
        public override string Author => BuildInfo.Author;
        public override string Version => BuildInfo.Version;
        public override string Link => BuildInfo.Link;

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> Key_Enable = new("enabled", "Enables this mod.", () => true);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> Key_Input = new("Joke_InputAll", "Joke config. Force input all.", () => false);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> Key_Output = new("Joke_OutputAll", "Joke config. Force output all.", () => false);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> Key_Abekonbe = new("Joke_Abekonbe", "Joke config. Force replace from input to/from output all.", () => false);

        private static ModConfiguration Config;

        public override void OnEngineInit()
        {
            Config = GetConfiguration();
            Config.Save(true);
            if (!Config.GetValue(Key_Enable)) return;
            Harmony harmony = new Harmony(BuildInfo.GUID);

            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(LogixHelper), "GetSide")]
        class Patch
        {
            static bool Prefix(LogixNode node, IConnectionElement member, ref ConnectPointSide __result)
            {
                if (member is LogixNode)
                {
                    __result = ConnectPointSide.Output;

                    if(Config.GetValue(Key_Input) || Config.GetValue(Key_Abekonbe))
                        __result = ConnectPointSide.Input;

                    return false;
                }
                FieldInfo fieldInfo = null;
                if (member.PointName != null)
                    fieldInfo = node.GetType().GetField(member.PointName);
                Type type = member.GetType();
                if (typeof(IInputElement).IsAssignableFrom(type))
                {
                    __result = ConnectPointSide.Input;

                    if(Config.GetValue(Key_Output) || Config.GetValue(Key_Abekonbe))
                        __result = ConnectPointSide.Output;

                    return false;
                }
                if (typeof(IOutputElement).IsAssignableFrom(type))
                {
                    __result = ConnectPointSide.Output;

                    if(Config.GetValue(Key_Input) || Config.GetValue(Key_Abekonbe))
                        __result = ConnectPointSide.Output;

                    return false;
                }
                throw new Exception("Invalid element: " + type?.ToString());
            }
        }
    }
}