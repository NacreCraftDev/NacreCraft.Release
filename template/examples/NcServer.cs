// NcServer: server side queries and actions.
//
// The running server is captured by the ModApi probe, so every member here
// throws until the main loop is up. Only call it from an event callback,
// and guard with IsAvailable when the call site is somewhere else.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Send a system message to everyone online.
    public static void BroadcastOnlineCount()
    {
        if (!NcServer.IsAvailable)
            return;

        NcServer.Broadcast($"players online: {NcPlayers.Count}/{NcPlayers.Max}");
    }

    // Run a built in command, as the console. The leading slash is optional.
    public static void SetDay()
    {
        NcServer.Execute("time set day");
    }

    // Read a few server wide values. Tps is derived from the average tick time.
    public static void Report()
    {
        Log.Info($"tick={NcServer.TickCount} tps={NcServer.Tps:F1} seed={NcServer.WorldSeed}");
    }

    // Change the weather. Both counts are durations in ticks, 0 means no change.
    public static void MakeItRain()
    {
        NcServer.SetWeather(clearTime: 0, rainTime: 6000, raining: true, thundering: false);
    }

    // Write the world to disk right now. Normally the server does this on its own.
    public static void Save()
    {
        NcServer.SaveAll();
    }
}
