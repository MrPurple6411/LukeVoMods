namespace FreeBeauty.Patches;
#if BELOWZERO

using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

[HarmonyPatch(typeof(Base))]
public static class BasePatches
{
    static bool patched = false;

    static readonly Dictionary<TechType, float> FaceHullStrReplacement = new()
                {
                    { TechType.BaseWindow, 0 },
                    { TechType.BaseGlassDome, 0 },
                    { TechType.BaseLargeGlassDome, 0 },
                };

    static readonly Dictionary<Base.CellType, float> CellHullStrReplacement = new()
                {
                    { Base.CellType.Observatory, -1.25f },
                };

    [HarmonyPatch(nameof(Awake)), HarmonyPostfix]
    public static void Awake()
    {
        if (patched)
        {
            return;
        }

        patched = true;

        // Face
        var facesStr = Base.FaceHullStrength;
        var faces = Base.FaceToRecipe;
        var len = faces.Length;
        for (int i = 0; i < len; i++)
        {
            if (FaceHullStrReplacement.TryGetValue(faces[i], out var str))
            {
                facesStr[i] = str;
            }
        }

        // Cell
        var cellsStr = Base.CellHullStrength;
        foreach (var cell in CellHullStrReplacement)
        {
            cellsStr[(int)cell.Key] = cell.Value;
        }
    }

    [HarmonyPatch(nameof(Base.GetHullStrength)), HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> GetHullStrengthTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var matcher = new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Base), "isGlass")),
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Ldelem_U1))
            .Set(OpCodes.Pop, null)
            .InsertAndAdvance(new CodeInstruction(OpCodes.Ldc_I4_0));

        return matcher.InstructionEnumeration();
    }
}

#endif