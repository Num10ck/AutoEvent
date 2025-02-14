﻿using Exiled.API.Features.Toys;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using MapGeneration.Distributors;
using MER.Lite.Objects;
using PluginAPI.Core.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoEvent.Games.CounterStrike
{
    internal class Functions
    {
        public static List<GameObject> GetAllSpawnpoints(SchematicObject GameMap)
        {
            if (GameMap is null)
            {
                DebugLogger.LogDebug("Map is null");
            }

            if (GameMap.AttachedBlocks is null)
            {
                DebugLogger.LogDebug("Attached Blocks is null");
            }

            return GameMap.AttachedBlocks.Where(x => x.name.Contains("Spawnpoint")).ToList();
        }
    }
}
