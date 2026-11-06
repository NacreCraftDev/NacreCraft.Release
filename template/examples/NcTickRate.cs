// NcTickRate: the world clock speed.
//
// Reach it through NcServer.TickRate. Freezing still ticks the levels, it
// only filters entities and random ticks, so LevelTick keeps firing.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Slow the world down; the floor is 1 tick per second.
    public static void SlowDown()
    {
        NcServer.TickRate.SetRate(10f);
    }

    // Freeze, advance a fixed number of ticks, unfreeze.
    public static void Advance()
    {
        var tickRate = NcServer.TickRate;
        tickRate.SetFrozen(true);
        tickRate.Step(100);
        tickRate.SetFrozen(false);
    }

    // Run without the tick sleep for a while.
    public static void FastForward()
    {
        NcServer.TickRate.Sprint(600);
    }
}
