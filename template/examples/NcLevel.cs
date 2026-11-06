// NcLevel: one dimension.
//
// A read only handle returned by NcWorld.Overworld / Nether / End and by
// NcWorld.Get("<namespace:path>"). It carries the dimension id, time, weather
// and height range. Block read and write stay on NcWorld and take the handle
// plus x y z, so no kernel type appears in this surface.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Name the dimension the overworld handle points at, and how far it has run.
    public static void WhereAmI()
    {
        var level = NcWorld.Overworld;
        Log.Info($"dimension {level.Dimension}, ticks {level.Ticks}");
    }

    // Time and weather live on the level, not on the server.
    public static void Night()
    {
        var level = NcWorld.Overworld;
        level.DayTime = 18000;
        level.RainLevel = 1f;
    }

    // Reach another dimension by id; null when it is not loaded right now.
    public static void NetherWeather()
    {
        var nether = NcWorld.Get("minecraft:the_nether");
        if (nether is null)
        {
            Log.Info("the nether is not loaded");
            return;
        }

        Log.Info($"nether rain {nether.RainLevel}, thunder {nether.ThunderLevel}");
    }

    // The height range tells you where blocks can exist in this dimension.
    public static void HeightRange()
    {
        var level = NcWorld.Overworld;
        Log.Info($"blocks live between y={level.MinBuildHeight} and y={level.MaxBuildHeight}");
    }

    // Force load a chunk so it keeps ticking with no player nearby.
    public static void KeepChunkAlive(int x, int z)
    {
        var level = NcWorld.Overworld;
        var changed = level.ForceLoadChunk(x, z);
        Log.Info($"force load {x},{z}: {changed}");
    }
}
