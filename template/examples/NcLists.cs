// NcLists: white list, operators, bans and server.properties.
//
// These are the kernel's live list objects, not copies. The settings view is
// NcLists.Settings; its setters only touch memory until Save runs.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Change the writable server.properties values, then persist them.
    public static void Harden()
    {
        var settings = NcLists.Settings;
        settings.SetDifficulty("hard");
        settings.SetWhiteList(true);
        settings.SetPlayerIdleTimeout(30);
        settings.Save();                 //only now does it reach the file
        Log.Info("server.properties updated");
    }

    // Read the settings and the list objects themselves.
    public static void ShowSettings()
    {
        Log.Info($"game mode {NcLists.Settings.Gamemode}, difficulty {NcLists.Settings.Difficulty}");
        Log.Info($"white list has {NcLists.WhiteList.Count} entries");
    }
}
