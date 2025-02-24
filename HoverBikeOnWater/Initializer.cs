﻿using LukeMods.Common;
using QModManager.API.ModLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukeMods.HoverBikeOnWater
{
    [QModCore]
    public static class Initializer
    {

        [QModPatch]
        public static void Init()
        {
            BaseInitializer.Init(nameof(HoverBikeOnWater), typeof(Initializer).Assembly);
            ModUtils.LogDebug("Loaded " + nameof(HoverBikeOnWater), false);
        }

    }
}
