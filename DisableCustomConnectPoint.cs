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
        public override string Name => "DisableCustomConnectPoint";
        public override string Author => "kazu0617";
        public override string Version => "1.1";
        public override string Link => "https://github.com/kazu0617/DisableCustomConnectPoint/";
        private readonly ModConfigurationKey<bool> Key_Enable = new ModConfigurationKey<bool>("enabled", "Enables this mod.", () => true);
        private readonly ModConfigurationKey<bool> Key_Input = new ModConfigurationKey<bool>("Joke_InputAll", "Joke config. Force input all.", () => false);
        private readonly ModConfigurationKey<bool> Key_Output = new ModConfigurationKey<bool>("Joke_OutputAll", "Joke config. Force output all.", () => false);
        private readonly ModConfigurationKey<bool> Key_Abekonbe = new ModConfigurationKey<bool>("Joke_Abekonbe", "Joke config. Force replace from input to/from output all.", () => false);
        public override ModConfigurationDefinition GetConfigurationDefinition()
        {
            List<ModConfigurationKey> keys = new List<ModConfigurationKey>();

            keys.Add(Key_Enable);
            keys.Add(Key_Input);
            keys.Add(Key_Output);
            keys.Add(Key_Abekonbe);
            return DefineConfiguration(new Version(1, 1), keys);

        }

        public override void OnEngineInit()
        {
            ModConfiguration config = GetConfiguration();
            if (!config.GetValue(Key_Enable)) return;
            Harmony harmony = new Harmony("net.kazu0617.DisableCustomConnectPoint");

            harmony.PatchAll();
            Msg("Hooks installed successfully!");
        }

        [HarmonyPatch(typeof(LogixHelper), "GetSide")]
        class Patch
        {
            static bool Prefix(LogixNode node, IConnectionElement member, ref ConnectPointSide __result)
            {
                if (member is LogixNode)
                {
                    __result = ConnectPointSide.Output;
                    return false;
                }
                FieldInfo fieldInfo = null;
                if (member.PointName != null)
                    fieldInfo = node.GetType().GetField(member.PointName);
                Type type = member.GetType();
                if (typeof(IInputElement).IsAssignableFrom(type))
                {
                    __result = ConnectPointSide.Input;
                    return false;
                }
                if (typeof(IOutputElement).IsAssignableFrom(type))
                {
                    __result = ConnectPointSide.Output;
                    return false;
                }
                throw new Exception("Invalid element: " + type?.ToString());
            }
        }

    }
}