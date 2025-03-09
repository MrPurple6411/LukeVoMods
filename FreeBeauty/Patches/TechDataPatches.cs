namespace FreeBeauty.Patches;
#if BELOWZERO

using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(TechData))]
public static class TechDataPatches
{
    static readonly Dictionary<TechType, TechType> IngredientReplacements = new Dictionary<TechType, TechType>()
    {
        { TechType.BaseCorridorGlassI, TechType.BaseCorridorI },
        { TechType.BaseCorridorGlassL, TechType.BaseCorridorL },
        { TechType.BaseObservatory, TechType.BaseRoom },

        { TechType.BaseGlassDome, TechType.BaseCorridorI },
        { TechType.BaseLargeGlassDome, TechType.BaseCorridorI },
        { TechType.BaseWindow, TechType.BaseCorridorI },
    };

    [HarmonyPatch(nameof(Cache)), HarmonyPrefix]
    public static void Cache()
    {
        var e = TechData.entries;

        var ingredientsId = TechData.propertyIngredients;
        foreach (var replacement in IngredientReplacements)
        {
            if (!e.TryGetValue(replacement.Key, out var src))
            {
                continue;
            }

            if (!e.TryGetValue(replacement.Value, out var dest))
            {
                continue;
            }

            src[ingredientsId] = dest[ingredientsId].Copy();
        }
    }

    static IEnumerable<KeyValuePair<TKey, TValue>> ToEnumerable<TKey, TValue>(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
    {
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }

}

#endif