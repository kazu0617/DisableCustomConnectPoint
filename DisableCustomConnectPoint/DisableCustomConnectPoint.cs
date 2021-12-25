using BaseX;
using FrooxEngine;
using FrooxEngine.LogiX;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Reflection;

namespace DisableCustomConnectPoint
{
    public class DisableCustomConnectPoint : NeosMod
    {
        public override string Name => "DisableCustomConnectPoint";
        public override string Author => "kazu0617";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/kazu0617/DisableCustomConnectPoint/";
        public override void OnEngineInit()
        {
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