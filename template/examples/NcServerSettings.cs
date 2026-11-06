// NcServerSettings: server.properties at runtime.
//
// Reach it through NcLists.Settings or NcServer.Settings. The setters only
// touch memory; call Save to flush them back to the file. Keys the kernel
// does not open to mods are not reachable here.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Read a few values.
    public static void Report()
    {
        var settings = NcLists.Settings;
        Log.Info($"port {settings.Port}, max {settings.MaxPlayers}, mode {settings.Gamemode}");
    }

    // Change the writable keys, then flush them.
    public static void Harden()
    {
        var settings = NcLists.Settings;
        settings.SetDifficulty("hard");
        settings.SetWhiteList(true);
        settings.SetPlayerIdleTimeout(30);
        settings.Save();
    }
}
