namespace FasterPdaScan;

using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

[HarmonyPatch(typeof(PDAScanner))]
public static class PdaScannerPatched
{
    [HarmonyPatch(nameof(PDAScanner.Scan)), HarmonyTranspiler, HarmonyPriority(Priority.First)]
    public static IEnumerable<CodeInstruction> ScanTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var matcher = new CodeMatcher(instructions);
        int count = 0;
        while (matcher.MatchForward(false, new CodeMatch(instruction => instruction.opcode == OpCodes.Stloc_S && instruction.operand is LocalVariableInfo localVar && localVar.LocalIndex == 5)).IsValid)
        {
            if (count > 0) 
                matcher.InsertAndAdvance(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PdaScannerPatched), nameof(MultiplyByConfigMultiplier)))).Advance(1);
            count++;            
        }
        if (count == 0)
        {
            Plugin.Logger.LogError("Failed to find the instruction to insert the multiplier");
        }
        else
        {
            Plugin.Logger.LogInfo($"Inserted multiplier instruction in {count-1} places.");
        }

        return matcher.InstructionEnumeration();
    }

    public static float MultiplyByConfigMultiplier(float originalValue)
    {
        try
        {
            return Mathf.Max(originalValue * Config.ScanTimeMultiplier, 0.01f);
        }
        catch (System.Exception e)
        {
            Plugin.Logger.LogError($"Error multiplying {originalValue} by config multiplier: {e}");
            return originalValue;
        }
    }
}
