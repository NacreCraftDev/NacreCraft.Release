// NcWorld: block and world state.
//
// Members without a level argument act on the overworld. Overloads that take
// an NcLevel work for the nether and the end as well, see NcWorld.Get.
//
// Coordinates are always plain x y z ints, no BlockPos on the public surface.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;
using NacreCraft.Registry.State;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Read one block. A null result means the chunk is not loaded right now.
    public static void Inspect(int x, int y, int z)
    {
        BlockState? state = NcWorld.GetBlock(x, y, z);
        if (state is null)
        {
            Log.Info("chunk is not loaded");
            return;
        }

        Log.Info($"block at {x} {y} {z} is {state.Value.Owner.Id}");
    }

    // Write one block. The call runs the full update chain and syncs clients,
    // the return value tells whether anything actually changed.
    public static void Replace(int x, int y, int z, string blockId)
    {
        var state = NcWorld.FindState(blockId);
        if (state is null)
            return;

        var changed = NcWorld.SetBlock(x, y, z, state);
        Log.Info($"set {blockId}: {changed}");
    }

    // Break a block the way a player would, drops and callbacks included.
    // Pass a player to get the player specific callbacks, null for environment.
    public static void Break(int x, int y, int z, NcPlayer? player = null)
    {
        NcWorld.BreakBlock(x, y, z, player);
    }

    // Time and weather are read and written through the world.
    public static void SetNoon()
    {
        NcWorld.DayTime = 6000;
    }

    public static void DryOut()
    {
        NcWorld.RainLevel = 0f;
        NcWorld.ThunderLevel = 0f;
    }

    // A handle is also how you reach another dimension, it carries the same
    // time and weather members as the overworld.
    public static void EmptyTheNether()
    {
        var nether = NcWorld.Get("minecraft:the_nether");
        if (nether is null)
            return;

        nether.RainLevel = 0f;
        Log.Info($"dimension {nether.Dimension} at tick {nether.Ticks}");
    }
}
